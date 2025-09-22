namespace SGE.Core.Entities;

public class Department : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the department.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the department.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique code for the department.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of employees associated with the department.
    /// </summary>
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}