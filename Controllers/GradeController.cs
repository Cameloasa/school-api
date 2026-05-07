using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    // GET: api/grades
    [HttpGet]
    public ActionResult<List<Grade>> GetAll()
    {
        try
        {
            var grades = _gradeService.GetGrades();
            return Ok(grades);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: api/grades/{id}
    [HttpGet("{id}")]
    public ActionResult<Grade> GetById(int id)
    {
        try
        {
            var grade = _gradeService.GetGradeById(id);
            if (grade == null)
            {
                return NotFound($"Grade with ID {id} not found");
            }
            return Ok(grade);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: api/grades/student/{studentId}
    [HttpGet("student/{studentId}")]
    public ActionResult<List<Grade>> GetByStudent(int studentId)
    {
        try
        {
            var grades = _gradeService.GetGradesByStudent(studentId);
            if (!grades.Any())
            {
                return NotFound($"No grades found for student with ID {studentId}");
            }
            return Ok(grades);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: api/grades/course-instance/{courseInstanceId}
    [HttpGet("course-instance/{courseInstanceId}")]
    public ActionResult<List<Grade>> GetByCourseInstance(int courseInstanceId)
    {
        try
        {
            var grades = _gradeService.GetGradesByCourseInstance(courseInstanceId);
            if (!grades.Any())
            {
                return NotFound($"No grades found for course instance with ID {courseInstanceId}");
            }
            return Ok(grades);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: api/grades/student/{studentId}/course-instance/{courseInstanceId}
    [HttpGet("student/{studentId}/course-instance/{courseInstanceId}")]
    public ActionResult<Grade> GetByStudentAndCourseInstance(int studentId, int courseInstanceId)
    {
        try
        {
            var grade = _gradeService.GetGradeByStudentAndCourseInstance(studentId, courseInstanceId);
            if (grade == null)
            {
                return NotFound($"Grade not found for student {studentId} and course instance {courseInstanceId}");
            }
            return Ok(grade);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // POST: api/grades
    [HttpPost]
    public ActionResult<Grade> Create([FromBody] CreateGradeRequest request)
    {
        try
        {
            var grade = _gradeService.CreateGrade(request);
            return CreatedAtAction(nameof(GetById), new { id = grade.GradeId }, grade);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // PUT: api/grades/{id}
    [HttpPut("{id}")]
    public ActionResult<Grade> Update(int id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            var updated = _gradeService.UpdateGrade(id, request);
            if (updated == null)
            {
                return NotFound($"Grade with ID {id} not found");
            }
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // DELETE: api/grades/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var deleted = _gradeService.DeleteGrade(id);
            if (!deleted)
            {
                return NotFound($"Grade with ID {id} not found");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}