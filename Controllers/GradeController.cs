using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("grades")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _service;

    public GradeController(IGradeService service)
    {
        _service = service;
    }

    // GET: /grades
    [HttpGet]
    public async Task<ActionResult> GetAllGrades()
    {
        var grades = await _service.GetGradesAsync();
        return Ok(grades);
    }

    // GET: /grades/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var grade = await _service.GetGradeByIdAsync(id);

        if (grade == null)
            return NotFound($"Grade with ID {id} not found");

        return Ok(grade);
    }

    // GET: /grades/student/{studentId}
    [HttpGet("student/{studentId}")]
    public async Task<ActionResult> GetByStudent(string studentId)
    {
        var grades = await _service.GetGradesByStudentIdAsync(studentId);

        if (!grades.Any())
            return NotFound($"No grades found for student {studentId}");

        return Ok(grades);
    }

    // GET: /grades/course-instance/{courseInstanceId}
    [HttpGet("course-instance/{courseInstanceId}")]
    public async Task<ActionResult> GetByCourseInstance(string courseInstanceId)
    {
        var grades = await _service.GetGradesByCourseInstanceIdAsync(courseInstanceId);

        if (!grades.Any())
            return NotFound($"No grades found for course instance {courseInstanceId}");

        return Ok(grades);
    }

    // POST: /grades
    [HttpPost]
    public async Task<ActionResult> CreateGrade([FromBody] CreateGradeRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            var created = await _service.CreateGradeAsync(request);
            return Created("/grades", created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PATCH: /grades/{id}
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateGrade(string id, [FromBody] UpdateGradeRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var updated = await _service.UpdateGradeAsync(id, request);

        if (updated == null)
            return NotFound($"Grade with ID {id} not found");

        return Ok(updated);
    }

    // DELETE: /grades/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteGradeAsync(id);

        if (!deleted)
            return NotFound($"Grade with ID {id} not found");

        return NoContent();
    }
}
