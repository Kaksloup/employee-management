using AutoMapper;
using SGE.Application.DTOs.Employees;
using SGE.Application.Interfaces.Repositories;
using SGE.Application.Interfaces.Services;
using SGE.Core.Entities;
using SGE.Core.Exceptions;

namespace SGE.Application.Services;

public class EmployeeService(IEmployeeRepository employeeRepository,
    IDepartmentRepository departmentRepository, IMapper mapper)  : IEmployeeService

{
    /// <summary>
    /// Asynchronously retrieves all employees from the repository and maps them to a collection of EmployeeDto.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of EmployeeDto objects.</returns>
    public async Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await employeeRepository.GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<EmployeeDto>>(list);
    }

    /// <summary>
    /// Asynchronously retrieves an employee by their unique identifier and maps it to an EmployeeDto.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an EmployeeDto object if found; otherwise, null.
    /// </returns>
    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var emp = await employeeRepository.GetByIdAsync(id, cancellationToken);
        return emp == null ? null : mapper.Map<EmployeeDto>(emp);
    }

    /// <summary>
    /// Asynchronously retrieves an employee by their email address and maps it to an EmployeeDto.
    /// </summary>
    /// <param name="email">The email address of the employee to retrieve.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the EmployeeDto if found; otherwise, null.</returns>
    public async Task<EmployeeDto?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var emp = await employeeRepository.GetByEmailAsync(email, cancellationToken);
        return emp == null ? null : mapper.Map<EmployeeDto>(emp);
    }

    /// <summary>
    /// Asynchronously retrieves employees belonging to a specific department and maps them to a collection of EmployeeDto.
    /// </summary>
    /// <param name="departmentId">The unique identifier of the department whose employees should be retrieved.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of EmployeeDto objects associated with the specified department.</returns>
    public async Task<IEnumerable<EmployeeDto>> GetByDepartmentAsync(int departmentId,
        CancellationToken cancellationToken = default)
    {
        var list = await employeeRepository.GetByDepartmentAsync(departmentId, cancellationToken);
        return mapper.Map<IEnumerable<EmployeeDto>>(list);
    }

    /// <summary>
    /// Asynchronously creates a new employee in the repository based on the provided data transfer object.
    /// </summary>
    /// <param name="dto">The data transfer object containing details of the employee to be created.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created EmployeeDto object.</returns>
    /// <exception cref="ApplicationException">Thrown if the specified department does not exist or if the email is already associated with another employee.</exception>
    public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto dto, CancellationToken cancellationToken = default)
    {
        var department = await departmentRepository.GetByIdAsync(dto.DepartmentId, cancellationToken);
        if (department == null)
            throw new DepartmentNotFoundException(dto.DepartmentId);
        
        var existingEmployee = await employeeRepository.GetByEmailAsync(dto.Email, cancellationToken);
        if (existingEmployee != null)
            throw new ApplicationException("Cet email existe déjà pour un autre employée");
        
        var entity = mapper.Map<Employee>(dto);
        await employeeRepository.AddAsync(entity, cancellationToken);
        return mapper.Map<EmployeeDto>(entity);
    }

    /// <summary>
    /// Asynchronously updates an employee's information in the repository using the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to update.</param>
    /// <param name="dto">An object containing the updated details of the employee.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the update operation was successful.</returns>
    public async Task<bool> UpdateAsync(int id, EmployeeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await employeeRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null) 
            throw new EmployeeNotFoundException(id);

        mapper.Map(dto, entity);
        await employeeRepository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    /// <summary>
    /// Asynchronously deletes an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to be deleted.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await employeeRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null) 
            throw new EmployeeNotFoundException(id);

        await employeeRepository.DeleteAsync(id, cancellationToken);
        return true;
    }
}