using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

namespace SchoolApi.Controllers;

[ApiController]
[Route("courses")]

public class CourseController(ICourseService service) : ControllerBase
{
    private readonly ICourseService _service = service;


    //get(courses)
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

    // Get /courses/{id}
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CourseResponse>> GetCourseById(string id)
    {
        try
        {
            var course = _service.GetCourseByIdAsync(id);
            if(course == null)
            {
                return NotFound($"Course with id {id} not found");
            }
            return Ok(course);
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    //Post /courses
    [HttpPost]
    public async Task<ActionResult<CourseResponse>> CreateCourse(
        [FromBody]CreateCourseRequest request)
    {
        if(!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        try
        {
            var course = await _service.CreateCourseAsync(request);
            return Created("/courses", course);
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    // Patch /courses/{id}
    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<CourseResponse>> UpdateCourse(
        string id,
        [FromBody] UpdateCourseRequest request)
    {
        // Validate id
        if(string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }
        // Validate request body
        if(!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        try        
        {
            //Call service to update course
            var updated = _service.UpdateCourseAsync(id, request);
            if(updated == null) 
                return NotFound($"Course with id {id} not found");
            
            return Ok(updated);
        }
        catch (Exception)
        {
             return StatusCode(500,"An error occured while processing the request");
        }
    }

    //delete /courses/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(string id)
    {
        try
        {
            var deleted = await _service.DeleteCourseAsync(id);
            if (!deleted)
                return NotFound($"Student with id {id} not found");

            return Ok();

        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }
}