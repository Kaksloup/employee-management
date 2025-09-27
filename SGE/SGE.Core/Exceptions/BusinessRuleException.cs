namespace SGE.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a business rule is violated within the system.
/// </summary>
/// <remarks>
/// This exception is intended to indicate issues where a specific business rule has been broken.
/// It includes a customizable message and defaults the error code to "BUSINESS_RULE_VIOLATION".
/// The HTTP status code associated with this exception is 400 (Bad Request).
/// </remarks>
public class BusinessRuleException : SgeException
{
    public BusinessRuleException(string message, string errorCode = "BUSINESS_RULE_VIOLATION")
        : base(message, errorCode, 400)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when an employee with a specified ID cannot be found.
/// </summary>
/// <remarks>
/// This exception is intended to indicate scenarios where a requested employee resource does not exist in the system.
/// It includes an ID reference for the missing employee, and the error code is set to "EMPLOYEE_NOT_FOUND".
/// The HTTP status code associated with this exception is 404 (Not Found).
/// </remarks>
public class EmployeeNotFoundException : SgeException
{
    public EmployeeNotFoundException(int employeeId)
        : base($"Employé avec l'ID {employeeId} introuvable.", "EMPLOYEE_NOT_FOUND", 404)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when a specific department cannot be found.
/// </summary>
/// <remarks>
/// This exception is intended to indicate that an operation involving a department has failed
/// because the specified department ID does not correspond to an existing department.
/// It includes a descriptive message, an error code set to "DEPARTMENT_NOT_FOUND",
/// and uses the HTTP status code 404 (Not Found).
/// </remarks>
public class DepartmentNotFoundException : SgeException
{
    public DepartmentNotFoundException(int departmentId)
        : base($"Département avec l'ID {departmentId} introuvable.", "DEPARTMENT_NOT_FOUND", 404)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when a leave request cannot be found in the system.
/// </summary>
/// <remarks>
/// This exception is designed to signal that a leave request, identified by its unique ID, was not found.
/// It includes a detailed error message, assigns an error code ("LEAVE_REQUEST_NOT_FOUND"),
/// and sets the HTTP status code to 404 (Not Found).
/// </remarks>
public class LeaveRequestNotFoundException : SgeException
{
    public LeaveRequestNotFoundException(int leaveRequestId)
        : base($"Demande de congé avec l'ID {leaveRequestId} introuvable.", "LEAVE_REQUEST_NOT_FOUND", 404)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when there are insufficient leave days available for a request.
/// </summary>
/// <remarks>
/// This exception is used to indicate that the number of leave days requested exceeds the number of leave days available for an employee.
/// It includes details about the required and available leave days and uses the error code "INSUFFICIENT_LEAVE_DAYS".
/// The associated HTTP status code is 400 (Bad Request).
/// </remarks>
public class InsufficientLeaveDaysException : SgeException
{
    public InsufficientLeaveDaysException(int requiredDays, int availableDays)
        : base($"Jours de congé insuffisants. Demandé: {requiredDays}, Disponible: {availableDays}", "INSUFFICIENT_LEAVE_DAYS", 400)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when a leave request conflicts with another existing leave request.
/// </summary>
/// <remarks>
/// This exception is utilized to indicate a situation where the requested leave period overlaps
/// with another leave request. It provides a descriptive message with the conflicting dates, and
/// includes an error code "CONFLICTING_LEAVE_REQUEST". The HTTP status code associated with
/// this exception is 409 (Conflict).
/// </remarks>
public class ConflictingLeaveRequestException : SgeException
{
    public ConflictingLeaveRequestException(DateTime startDate, DateTime endDate)
        : base($"Conflit de congé détecté pour la période du {startDate:dd/MM/yyyy} au {endDate:dd/MM/yyyy}", "CONFLICTING_LEAVE_REQUEST", 409)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when an invalid status transition occurs for a leave request.
/// </summary>
/// <remarks>
/// This exception is used to indicate that a transition from one leave status to another is not allowed.
/// It includes a detailed error message with the current and attempted statuses, an error code
/// "INVALID_STATUS_TRANSITION", and is associated with the HTTP status code 400 (Bad Request).
/// </remarks>
public class InvalidLeaveStatusTransitionException : SgeException
{
    public InvalidLeaveStatusTransitionException(string currentStatus, string newStatus)
        : base($"Transition de statut invalide de '{currentStatus}' vers '{newStatus}'", "INVALID_STATUS_TRANSITION", 400)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when one or more validation errors occur in the system.
/// </summary>
/// <remarks>
/// This exception is intended to encapsulate validation errors that happen when incoming data
/// does not meet the expected format, constraints, or business rules.
/// It provides a collection of validation errors associated with specific properties and can
/// be instantiated with either multiple errors or a single property error.
/// The default error code for this exception is "VALIDATION_ERROR", and it corresponds to the HTTP
/// status code 400 (Bad Request).
/// </remarks>
public class ValidationException : SgeException
{
    public Dictionary<string, List<string>> Errors { get; }

    public ValidationException(Dictionary<string, List<string>> errors)
        : base("Une ou plusieurs erreurs de validation sont survenues.", "VALIDATION_ERROR", 400)
    {
        Errors = errors;
    }

    public ValidationException(string propertyName, string errorMessage)
        : this(new Dictionary<string, List<string>> { { propertyName, new List<string> { errorMessage } } })
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when an issue related to attendance operations occurs.
/// </summary>
/// <remarks>
/// This exception is designed to handle errors specific to attendance functionality within the system.
/// It includes a customizable message and defaults the error code to "ATTENDANCE_ERROR".
/// The HTTP status code associated with this exception is 400 (Bad Request).
/// </remarks>
public class AttendanceException : SgeException
{
    public AttendanceException(string message, string errorCode = "ATTENDANCE_ERROR") 
        : base(message, errorCode, 400)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when an attempt is made to clock in an employee who is already clocked in.
/// </summary>
/// <remarks>
/// This exception is used to indicate that an employee already exists in the system as currently clocked in,
/// preventing duplicate clock-in actions for the same employee.
/// It provides a detailed message including the employee ID, an error code "ALREADY_CLOCKED_IN",
/// and an HTTP status code of 409 (Conflict).
/// </remarks>
public class AlreadyClockedInException : SgeException
{
    public AlreadyClockedInException(int employeeId) 
        : base($"L'employé {employeeId} est déjà pointé.", "ALREADY_CLOCKED_IN", 409)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when an employee has not clocked in as required.
/// </summary>
/// <remarks>
/// This exception is intended to indicate a scenario where an operation cannot be performed
/// because the employee is not recognized as having clocked in.
/// It includes details about the employee ID, a specific error code ("NOT_CLOCKED_IN"),
/// and associates the exception with the HTTP status code 400 (Bad Request).
/// </remarks>
public class NotClockedInException : SgeException
{
    public NotClockedInException(int employeeId) 
        : base($"L'employé {employeeId} n'a pas pointé à l'arrivée.", "NOT_CLOCKED_IN", 400)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when an attempt is made to create or register a department
/// with a name that already exists within the system.
/// </summary>
/// <remarks>
/// This exception is intended to signal violations of unique constraints with respect to department names.
/// It includes a message specifying the conflicting department name, an error code of "DEPARTMENT_NAME_EXISTS",
/// and an associated HTTP status code of 409 (Conflict).
/// </remarks>
public class DuplicateDepartmentNameException : SgeException
{
    public DuplicateDepartmentNameException(string departmentName) 
        : base($"Le nom du département '{departmentName}' existe déjà.", "DEPARTMENT_NAME_EXISTS", 409)
    {
    }
}

/// <summary>
/// Represents an exception that is thrown when invalid data is provided for an employee within the system.
/// </summary>
/// <remarks>
/// This exception is specifically used to indicate issues where employee-related data is incorrect or does not satisfy the required validation rules.
/// It provides a custom error code "INVALID_EMPLOYEE_DATA" and is associated with a 400 (Bad Request) HTTP status code.
/// </remarks>
public class InvalidEmployeeDataException : SgeException
{
    public InvalidEmployeeDataException(string message) 
        : base(message, "INVALID_EMPLOYEE_DATA", 400)
    {
    }
}
