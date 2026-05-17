using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }

    // GET /courses
    [HttpGet]
    public async Task<ActionResult<List<CourseResponse>>> GetCourses()
    {
        try
        {
            var courses = await _service.GetCoursesAsync();
            return Ok(courses);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // GET /courses/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseResponse>> GetCourseById(string id)
    {
        try
        {
            var course = await _service.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound($"Course with id {id} not found");

            return Ok(course);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // POST /courses
    [HttpPost]
    public async Task<ActionResult<CourseResponse>> CreateCourse([FromBody] CreateCourseRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            var course = await _service.CreateCourseAsync(request);
            return Created($"/courses/{course.CourseId}", course);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // PATCH /courses/{id}
    [HttpPatch("{id}")]
    public async Task<ActionResult<CourseResponse>> UpdateCourse(string id, [FromBody] UpdateCourseRequest request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Id is required");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            var updated = await _service.UpdateCourseAsync(id, request);
            if (updated == null)
                return NotFound($"Course with id {id} not found");

            return Ok(updated);
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }

    // DELETE /courses/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(string id)
    {
        try
        {
            var deleted = await _service.DeleteCourseAsync(id);
            if (!deleted)
                return NotFound($"Course with id {id} not found");

            return Ok();
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request");
        }
    }
}
