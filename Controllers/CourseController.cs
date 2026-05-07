using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;

[ApiController]
[Route("[controller]")]

public class CourseController(ICourseService service) : ControllerBase
{
    private readonly ICourseService _service = service;


    [HttpGet]
    public ActionResult<List<Course>> GetCourses()
    {
        return Ok(_service.GetCourses());
    }

    [HttpGet]
    [Route("/{id}")]
    public ActionResult<Course?> GetCourseById(int id)
    {
        try
        {
            Course? found = _service.GetCourseById(id);
            if(found == null)
            {
                return NotFound($"Course with id {id} not found");
            }
            return Ok(found);
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpPost]
    public ActionResult<Course?> CreateCourse([FromBody]CreateCourseRequest request)
    {
        try
        {
            Course newCourse = _service.CreateCourse(request);
            return Created("/courses", newCourse);
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpPatch]
    [Route("{id}")]
    public ActionResult<Course> UpdateCourse(int id, [FromBody]CreateCourseRequest request)
    {
        try
        {
            Course? updatedCourse = _service.UpdateCourse(id, request);
            if(updatedCourse == null)
            {
                return NotFound($"Course with id {id} not found");
            }
            return Ok(updatedCourse);
        }
        catch (Exception)
        {
             return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteCourse(int id)
    {
        try
        {
            Course? found = _service.GetCourseById(id);
            if(found == null)
            {
                return NotFound($"Course with id {id} not found");
            }
            _service.DeleteCourse(id);
            return Ok();

        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }
}