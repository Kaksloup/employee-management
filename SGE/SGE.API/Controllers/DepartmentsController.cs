using Microsoft.AspNetCore.Mvc;
using SGE.Application.DTOs;
using SGE.Application.Interfaces.Services;

namespace SGE.API.Controllers;

/// <summary>
/// The DepartmentsController class handles HTTP requests related to department operations
/// such as retrieving, creating, updating, and deleting departments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(IDepartmentService departmentService) : ControllerBase
{
    /// <summary>
    /// Retrieves all departments.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ActionResult"/> containing an enumerable collection of <see cref="DepartmentDto"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll(CancellationToken cancellationToken)
    {
        var depts = await departmentService.GetAllAsync(cancellationToken);
        return Ok(depts);
    }
    
    /// <summary>
    /// Retrieves a specific department by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ActionResult"/> containing the <see cref="DepartmentDto"/> of the specified department if found, otherwise a NotFound result.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dept = await departmentService.GetByIdAsync(id, cancellationToken);
        if (dept == null) return NotFound();
        return Ok(dept);
    }

    /// <summary>
    /// Creates a new department based on the provided data transfer object.
    /// </summary>
    /// <param name="dto">The data transfer object containing the details of the department to create.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ActionResult"/> containing the created <see cref="DepartmentDto"/> with its unique identifier and other details.</returns> 
    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create(DepartmentCreateDto dto, CancellationToken cancellationToken)
    {
        var created = await departmentService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing department with the given identifier and updated details.
    /// </summary>
    /// <param name="id">The unique identifier of the department to update.</param>
    /// <param name="dto">The data transfer object containing the updated details of the department.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the update operation. Returns <see cref="NoContentResult"/> if successful, or <see cref="NotFoundResult"/> if the department is not found.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentUpdateDto dto, CancellationToken cancellationToken)
    {
        var ok = await departmentService.UpdateAsync(id, dto, cancellationToken);
        if (!ok) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific department by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns NoContent if successful, otherwise NotFound if the department does not exist.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var ok = await departmentService.DeleteAsync(id, cancellationToken);
        if (!ok) return NotFound();
        return NoContent();
    }
}