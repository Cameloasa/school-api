using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;
[ApiController]
[Route("students")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)
    {
        _service = service;
    }

    // GET /students
    [HttpGet]
    public async Task<ActionResult<List<StudentResponse>>> GetStudents()
    {
        try
        {
            var students = await _service.GetStudentsAsync();
            return Ok(students);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // GET /students/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentResponse>> GetStudentById(string id)
    {
        try
        {
            var student = await _service.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound($"Student with id {id} not found");

            return Ok(student);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // PATCH /students/{id}
    [HttpPatch("{id}")]
    public async Task<ActionResult<StudentResponse>> UpdateStudent(
        string id,
        [FromBody] UpdateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Invalid student ID");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            var updated = await _service.UpdateStudentAsync(id, request);
            if (updated == null)
                return NotFound($"Student with id {id} not found");

            return Ok(updated);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // DELETE /students/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(string id)
    {
        try
        {
            var deleted = await _service.DeleteStudentAsync(id);
            if (!deleted)
                return NotFound($"Student with id {id} not found");

            return Ok();
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }
}
