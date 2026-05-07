using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Services;

[ApiController]
[Route("students")]
public class StudentController(IStudentService service):ControllerBase
{
    private readonly IStudentService _service = service;

    [HttpGet]
    public ActionResult<List<Student>> GetStudents()
    {
        return Ok(_service.GetStudents());
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Student?> GetStudentById(int id)

    {
        try{
            Student? found = _service.GetStudentById(id);

            if(found == null)
            {
            return NotFound($"Student with id {id} not found");
            }  
        return Ok(found); 
        }
        
        catch(Exception)
        {
            return StatusCode(500,"An error occured while processing the request");
        }
    }

    [HttpPost]
    public ActionResult<Student?> CreateStudent([FromBody]CreateStudentRequest request)
    {
        try
        {
            Student newStudent = _service.CreateStudent(request);
            return Created("/students", newStudent);
        }
        catch(Exception)
        {
            return StatusCode(500,"An error occured while processing the request ");
        }
    }

    [HttpPatch]
    [Route("{id}")]
    public ActionResult<Student?> UpdateStudent(int id, [FromBody]CreateStudentRequest request)
    {
        try
        {
            Student? updatedStudent = _service.UpdateStudent(id,request);
            if(updatedStudent == null)
            {
                return NotFound($"Student with id {id} not found");
            }
            return Ok(updatedStudent);
        }
        catch(Exception)
        {
          return StatusCode(500,"An error occured while processing the request ");  
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteStudent(int id)
    {
        try
        {
            Student? found = _service.GetStudentById(id);
            if (found == null)
            {
                return NotFound($"student with id {id} not found");
            }
            _service.DeleteStudent(id);
            return Ok();

        }
        catch(Exception)
        {
            return StatusCode(500,"An error occured while processing the request ");
        }
    }

    
}