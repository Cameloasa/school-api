using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models.Requests;
using SchoolApi.Services;
using SchoolApi.Validators;

[ApiController]
[Route("course-instances")]
public class CourseInstanceController : ControllerBase
{
    private readonly ICourseInstanceService _service;

    public CourseInstanceController(ICourseInstanceService service)
    {
        _service = service;
    }

    // GET ALL
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var instances = await _service.GetInstancesAsync();
        return Ok(instances);
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var instance = await _service.GetInstanceByIdAsync(id);

        if (instance == null)
            return NotFound($"Course instance with id {id} not found");

        return Ok(instance);
    }

    // GET BY COURSE ID
    [HttpGet("course/{courseId}")]
    public async Task<ActionResult> GetByCourse(string courseId)
    {
        var instances = await _service.GetInstancesByCourseIdAsync(courseId);

        if (!instances.Any())
            return NotFound($"No course instances found for course {courseId}");

        return Ok(instances);
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCourseInstanceRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var validator = new DateValidation(request.StartDate, request.EndDate);
        var errors = validator.Validate();

        if (errors.Any())
            return BadRequest(errors);

        try
        {
            var created = await _service.CreateInstanceAsync(request);
            return Created("/course-instances", created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // UPDATE
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdateCourseInstanceRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var validator = new DateValidation(request.StartDate, request.EndDate);
        var errors = validator.Validate();

        if (errors.Any())
            return BadRequest(errors);

        try
        {
            var updated = await _service.UpdateInstanceAsync(id, request);

            if (updated == null)
                return NotFound($"Course instance with id {id} not found");

            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteInstanceAsync(id);

        if (!deleted)
            return NotFound($"Course instance with id {id} not found");

        return Ok();
    }
}
