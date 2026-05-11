using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

[ApiController]
[Route("course-instances")]
public class CourseInstanceController(ICourseInstanceService service):ControllerBase
{
    private readonly ICourseInstanceService _service = service;

    [HttpGet]
    public ActionResult<List<CourseInstance>> GetCourseInstances()
    {
        return Ok(_service.GetCourseInstances());
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<CourseInstance?> GetCourseInstance(string id)
    {
        try{
            CourseInstance? found = _service.GetCourseInstanceById(id);
            if (found == null)
            {
                return NotFound($"Course instance with id {id} not found");
            }
            return Ok(found);
        }
        catch (ArgumentException)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpPost]
    public ActionResult<CourseInstance> CreateCourseInstance([FromBody]CreateCourseInstancesRequest request)
    {
        try
        {
            CourseInstance newCourseInstance = _service.CreateCourseInstance(request);
            return Created("/course-instances", newCourseInstance);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpPatch]
    [Route("{id}")]
    public ActionResult<CourseInstance?> UpdateCourseInstance(string id, [FromBody]CreateCourseInstancesRequest request)
    {
        try
        {
            CourseInstance? updatedCourseInstance = _service.UpdateCourseInstance(id, request);
            if (updatedCourseInstance == null)
            {
                return NotFound($"Course instance with id {id} not found");
            }
            return Ok(updatedCourseInstance);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteCourseInstance(string id)
    {
        try
        {
            CourseInstance? found = _service.GetCourseInstanceById(id);
            if (found == null)
            {
                return NotFound($"Course instance with id {id} not found");
            }
            _service.DeleteCourseInstance(id);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpGet]
    [Route("student/{studentId}")] 
    public ActionResult<IEnumerable<CourseInstance>> GetByStudent(string studentId)
    {
        try
        {
            IEnumerable<CourseInstance> found = _service.GetByStudent(studentId);
            
            if (found == null || !found.Any())
            {
                return NotFound($"No course instances found for student with id {studentId}");
            }
            
            return Ok(found);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }   

    [HttpGet]
    [Route("course/{courseId}")]
    public ActionResult<IEnumerable<CourseInstance>> GetByCourse(string courseId)
    {
        try
        {
            IEnumerable<CourseInstance> found = _service.GetByCourse(courseId);
            
            if (found == null || !found.Any())
            {
                return NotFound($"No course instances found for course with id {courseId}");
            }
            
            return Ok(found);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }
}
