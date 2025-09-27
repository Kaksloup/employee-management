using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGE.Application.DTOs.LeaveRequests;
using SGE.Application.Interfaces.Repositories;
using SGE.Application.Interfaces.Services;
using SGE.Core.Entities;
using SGE.Core.Enums;
using SGE.Core.Exceptions;

namespace SGE.Application.Services;

public class LeaveRequestService(
    IEmployeeRepository employeeRepository, 
    ILeaveRequestRepository leaveRequestRepository,
    IMapper mapper)
    : ILeaveRequestService
{
    /// <summary>
    /// Creates a new leave request asynchronously.
    /// </summary>
    /// <param name="dto">
    /// The data transfer object containing the details of the leave request to be created.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The details of the newly created leave request, wrapped in a LeaveRequestDto.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if the referenced employee does not exist.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the leave request has invalid dates, such as an end date earlier than the start date,
    /// a start date in the past, or conflicting with an existing approved leave request.
    /// </exception>
    public async Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto dto,
        CancellationToken cancellationToken = default)
    {
        var employee = await employeeRepository.GetByIdAsync(dto.EmployeeId, cancellationToken);
        if (employee is null)
            throw new EmployeeNotFoundException(dto.EmployeeId);

        if (dto.EndDate < dto.StartDate)
            throw new ValidationException("EndDate", "La date de fin doit être supérieure à la date de début.");

        if (dto.StartDate < DateTime.Today)
            throw new ValidationException("StartDate", "La date de début doit être supérieure ou égale à la date de jour.");
        
        var daysRequested = CalculateBusinessDays(dto.StartDate, dto.EndDate);
        
        var hasConflict = await HasConflictingLeaveAsync(dto.EmployeeId, dto.StartDate, dto.EndDate, cancellationToken: cancellationToken);
        if (hasConflict)
            throw new ConflictingLeaveRequestException(dto.StartDate, dto.EndDate);
        
        var entity = mapper.Map<LeaveRequest>(dto);
        entity.DaysRequested = daysRequested;

        await leaveRequestRepository.AddAsync(entity, cancellationToken);
        return mapper.Map<LeaveRequestDto>(entity);
    }

    /// <summary>
    /// Retrieves the details of a leave request by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the leave request to be retrieved.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The details of the leave request wrapped in a LeaveRequestDto, or null if no leave request with the specified ID exists.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<LeaveRequestDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var leave = await leaveRequestRepository.GetByIdAsync(id, q => q.Include( x => x.Employee), cancellationToken);
        return leave == null ? null : mapper.Map<LeaveRequestDto>(leave);
    }

    /// <summary>
    /// Retrieves the leave requests associated with a specific employee asynchronously.
    /// </summary>
    /// <param name="employeeId">
    /// The unique identifier of the employee whose leave requests are to be retrieved.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A collection of leave request details wrapped in LeaveRequestDto objects.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeAsync(int employeeId,
        CancellationToken cancellationToken = default)
    {
        var list = await leaveRequestRepository.GetByEmployeeAsync(employeeId, cancellationToken);
        return mapper.Map<IEnumerable<LeaveRequestDto>>(list);
    }

    /// <summary>
    /// Retrieves a collection of leave requests based on the specified status asynchronously.
    /// </summary>
    /// <param name="status">
    /// The status of the leave requests to filter by, such as Pending, Approved, Rejected, or Cancelled.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A collection of leave requests matching the specified status, wrapped in LeaveRequestDto objects.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByStatusAsync(LeaveStatus status,
        CancellationToken cancellationToken = default)
    {
        var employees = await leaveRequestRepository.FindAsync(x => x.Status == status, cancellationToken);
        return mapper.Map<IEnumerable<LeaveRequestDto>>(employees);
    }

    /// <summary>
    /// Retrieves all leave requests with a status of pending asynchronously.
    /// </summary>
    /// <returns>
    /// A collection of leave requests that are currently pending, wrapped in LeaveRequestDto objects.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<IEnumerable<LeaveRequestDto>> GetPendingLeaveRequestsAsync()
    {
        var employees = await leaveRequestRepository.FindAsync(x => x.Status == LeaveStatus.Pending);
        return mapper.Map<IEnumerable<LeaveRequestDto>>(employees);    
    }

    /// <summary>
    /// Updates the status of an existing leave request asynchronously.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the leave request to be updated.
    /// </param>
    /// <param name="dto">
    /// An object containing the updated status and optional manager comments for the leave request.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A boolean value indicating whether the operation was successful.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<bool> UpdateStatusAsync(int id, LeaveRequestUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        var entity = await leaveRequestRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null) 
            throw new LeaveRequestNotFoundException(id);

        entity.Status = dto.Status;
        entity.ManagerComments = dto.ManagerComments;
        entity.ReviewedAt = DateTime.UtcNow;

        await leaveRequestRepository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves the remaining leave days for a specific employee in a given year asynchronously.
    /// </summary>
    /// <param name="employeeId">
    /// The unique identifier of the employee for whom the remaining leave days are being retrieved.
    /// </param>
    /// <param name="year">
    /// The year for which the remaining leave days are being calculated.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The total number of remaining leave days for the specified employee and year.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<int> GetRemainingLeaveDaysAsync(int employeeId, int year,
        CancellationToken cancellationToken = default)
    {
        // Supposons 25 jours de congés annuels par défaut
        const int totalAnnualLeaveDays = 25;
        
       var approvedLeaves = await leaveRequestRepository.FindAsync(x => x.EmployeeId == employeeId 
                                                                && x.Status == LeaveStatus.Approved
                                                                && x.LeaveType == LeaveType.Annual
                                                                && x.StartDate.Year == year 
           , cancellationToken);

        var dayApprouved =  approvedLeaves.Sum(lr => lr.DaysRequested);

        return Math.Max(0, totalAnnualLeaveDays - dayApprouved);
    }

    /// <summary>
    /// Checks if there are any conflicting leave requests for an employee within the specified date range.
    /// </summary>
    /// <param name="employeeId">
    /// The ID of the employee for whom the check is being performed.
    /// </param>
    /// <param name="startDate">
    /// The start date of the leave period to verify for conflicts.
    /// </param>
    /// <param name="endDate">
    /// The end date of the leave period to verify for conflicts.
    /// </param>
    /// <param name="excludeRequestId">
    /// An optional leave request ID to exclude from the conflict check, typically used when updating an existing leave request.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests during the operation.
    /// </param>
    /// <returns>
    /// A boolean value indicating whether any conflicting leave requests exist.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented.
    /// </exception>
    public async Task<bool> HasConflictingLeaveAsync(int employeeId, DateTime startDate, DateTime endDate,
        int? excludeRequestId = null,
        CancellationToken cancellationToken = default)
    {
        var leaves = await leaveRequestRepository.FindAsync( x => x.EmployeeId == employeeId 
                                                            && x.Status == LeaveStatus.Approved 
                                                            && ((x.StartDate <= endDate && x.EndDate >= startDate)), cancellationToken);;

        if (excludeRequestId.HasValue)
            leaves = leaves.Where(lr => lr.Id != excludeRequestId.Value);

        return leaves.Any();
    }

    private int CalculateBusinessDays(DateTime startDate, DateTime endDate)
    {
        int businessDays = 0;
        DateTime current = startDate;

        while (current <= endDate)
        {
            if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
                businessDays++;

            current = current.AddDays(1);
        }

        return businessDays;
    }

}