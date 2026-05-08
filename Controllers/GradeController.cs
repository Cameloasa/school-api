using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("grades")]
public class GradeController(IGradeService service) : ControllerBase
{
    // GET: /grades
    [HttpGet]
    public ActionResult<IEnumerable<Grade>> GetAllGrades()
    {
        try
        {
            return Ok(service.GetGrades());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: /grades/{id}
    [HttpGet("{id}")]
    public ActionResult<Grade?> GetById(string id)
    {
        try
        {
            Grade? grade = service.GetGradeById(id);
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

    // GET: /grades/student/{studentId}
    [HttpGet("student/{studentId}")]
    public ActionResult<IEnumerable<Grade>> GetByStudent(string studentId)
    {
        try
        {
            var grades = service.GetGradesByStudent(studentId);
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

    // GET: /grades/course-instance/{courseInstanceId}
    [HttpGet("course-instance/{courseInstanceId}")]
    public ActionResult<IEnumerable<Grade>> GetByCourseInstance(string courseInstanceId)
    {
        try
        {
            var grades = service.GetGradesByCourseInstance(courseInstanceId);
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

    // GET: /grades/student/{studentId}/course-instance/{courseInstanceId}
    [HttpGet("student/{studentId}/course-instance/{courseInstanceId}")]
    public ActionResult<Grade> GetByStudentAndCourseInstance(string studentId, string courseInstanceId)
    {
        try
        {
            var grade = service.GetGradeByStudentAndCourseInstance(studentId, courseInstanceId);
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

    // POST: /grades
    [HttpPost]
    public ActionResult<Grade?> Create([FromBody] CreateGradeRequest request)
    {
        try
        {
            Grade? newGrade = service.CreateGrade(request);
            return Created("/grades", newGrade);
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

    // PATCH: /grades/{id}
    [HttpPatch("{id}")]
    public ActionResult<Grade> Update(string id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            var updated = service.UpdateGrade(id, request);
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

    // DELETE: /grades/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        try
        {
            var deleted = service.DeleteGrade(id);
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