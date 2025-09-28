using AutoMapper;
using SGE.Application.DTOs.Attendances;
using SGE.Application.DTOs.Departments;
using SGE.Application.DTOs.Employees;
using SGE.Application.DTOs.LeaveRequests;
using SGE.Application.DTOs.Users;
using SGE.Core.Entities;

namespace SGE.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentCreateDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
         CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

        CreateMap<EmployeeCreateDto, Employee>();
        CreateMap<EmployeeUpdateDto, Employee>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // ignore nulls
        
        CreateMap<AttendanceCreateDto, Attendance>()
            .ForMember(dest => dest.BreakDuration, opt => opt.MapFrom(src => TimeSpan.FromHours(src.BreakDurationHours)));
        CreateMap<Attendance, AttendanceDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"));

        CreateMap<LeaveRequestCreateDto, LeaveRequest>();
        CreateMap<LeaveRequest, LeaveRequestDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
            .ForMember(dest => dest.LeaveTypeName, opt => opt.MapFrom(src => src.LeaveType.ToString()))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()));
       
        // RegisterDto → ApplicationUser
        CreateMap<RegisterDto, ApplicationUser>()
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        // ApplicationUser → UserDto
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Sera rempli manuellement

    }
}