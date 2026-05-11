using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("grades")]
public class GradeController(IGradeService service) : ControllerBase
{
    private readonly IGradeService _service = service;
    // GET: /grades
    [HttpGet]
    public ActionResult<IEnumerable<Grade>> GetAllGrades()
    {
        try
        {
            return Ok(_service.GetGrades());
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
            Grade? found = _service.GetGradeById(id);
            if (found == null)
            {
                return NotFound($"Grade with ID {id} not found");
            }
            return Ok(found);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: /grades/student/{studentId}
    [HttpGet]
    [Route("student/{studentId}")]
    public ActionResult<IEnumerable<Grade>> GetByStudent(string studentId)
    {
        try
        {
            IEnumerable<Grade> found = _service.GetGradesByStudent(studentId);
            if (found == null || !found.Any())
            {
                return NotFound($"No grades found for student with ID {studentId}");
            }
            return Ok(found);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: /grades/course-instance/{courseInstanceId}
    [HttpGet]
    [Route("course-instance/{courseInstanceId}")]
    public ActionResult<IEnumerable<Grade>> GetByCourseInstance(string courseInstanceId)
    {
        try
        {
            IEnumerable<Grade> found = _service.GetGradesByCourseInstance(courseInstanceId);
            if (found == null || !found.Any())
            {
                return NotFound($"No grades found for course instance with ID {courseInstanceId}");
            }
            return Ok(found);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // GET: /grades/student/{studentId}/course-instance/{courseInstanceId}
    [HttpGet]
    [Route("student/{studentId}/course-instance/{courseInstanceId}")]
    public ActionResult<Grade> GetByStudentAndCourseInstance(string studentId, string courseInstanceId)
    {
        try
        {
            Grade? found = _service.GetGradeByStudentAndCourseInstance(studentId, courseInstanceId);
            if (found == null)
            {
                return NotFound($"Grade not found for student {studentId} and course instance {courseInstanceId}");
            }
            return Ok(found);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    // POST: /grades
    [HttpPost]
    public ActionResult<List<Grade>> CreateGrade([FromBody] CreateGradeRequest request)
    {
        try
        {
            List<Grade> newGrades = _service.CreateGrade(request);
            return Created("/grades", newGrades);
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
    public ActionResult<Grade?> UpdateGrade(string id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            Grade? updatedGrade = _service.UpdateGrade(id, request);
            if (updatedGrade == null)
            {
                return NotFound($"Grade with ID {id} not found");
            }
            return Ok(updatedGrade);
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
            Grade? found = _service.GetGradeById(id);
            if (found == null)
            {
                return NotFound($"Grade with ID {id} not found");
            }
            _service.DeleteGrade(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}