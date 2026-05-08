using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;

[ApiController]
[Route("course-instances")]
public class CourseInstanceController(ICourseInstanceService courseInstanceService):ControllerBase
{
    private readonly ICourseInstanceService _courseInstanceService = courseInstanceService;

    [HttpGet]
    public ActionResult<List<CourseInstance>> GetCourseInstances()
    {
        return Ok(_courseInstanceService.GetCourseInstances());
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<CourseInstance?> GetCourseInstance(string id)
    {
        try{
            CourseInstance? found = _courseInstanceService.GetById(id);
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
            CourseInstance newCourseInstance = _courseInstanceService.CreateCourseInstance(request);
            return Created("/courseinstances", newCourseInstance);
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
            CourseInstance? updatedCourseInstance = _courseInstanceService.UpdateCourseInstance(id, request);
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
            CourseInstance? found = _courseInstanceService.GetById(id);
            if (found == null)
            {
                return NotFound($"Course instance with id {id} not found");
            }
            _courseInstanceService.DeleteCourseInstance(id);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpGet]
    [Route("by-student/{studentId}")] 
    public ActionResult<IEnumerable<CourseInstance>> GetByStudent(string studentId)
    {
        try
        {
            IEnumerable<CourseInstance> found = _courseInstanceService.GetByStudent(studentId);
            
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
    [Route("by-course/{courseId}")]
    public ActionResult<IEnumerable<CourseInstance>> GetByCourse(string courseId)
    {
        try
        {
            IEnumerable<CourseInstance> found = _courseInstanceService.GetByCourse(courseId);
            
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
