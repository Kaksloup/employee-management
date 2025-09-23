namespace SGE.Application.DTOs;

public class DepartmentDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the department.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the department.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the code that uniquely identifies the department.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the department.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}