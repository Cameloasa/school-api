namespace SchoolApi.Services;

using SchoolApi.Mappers;
using SchoolApi.Models;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

// =========================
//      INTERFACE
// =========================
public interface IStudentService
{
    Task<List<StudentResponse>> GetStudentsAsync();
    Task<StudentResponse?> GetStudentByIdAsync(string id);
    Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request, string userId);
    Task<StudentResponse?> UpdateStudentAsync(string id, UpdateStudentRequest request);
    Task<bool> DeleteStudentAsync(string id);
}

// =========================
//      IMPLEMENTATION
// =========================
public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository repo)
    {
        _studentRepository = repo;
    }

    //get all
    public async Task<List<StudentResponse>> GetStudentsAsync()
    {
        try{
        var students = await _studentRepository.GetStudentsAsync();
        return students.Select(StudentMapper.ToResponse).ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving students", ex);
        }  
    }

    //get by id
    public async Task<StudentResponse?> GetStudentByIdAsync(string id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        return student is null ? null : StudentMapper.ToResponse(student);
    }

    //create
    public async Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request, string userId)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserId = userId
        };

        var created = await _studentRepository.AddStudentAsync(student);
        return StudentMapper.ToResponse(created);
    }

    //update
    public async Task<StudentResponse?> UpdateStudentAsync(string id, UpdateStudentRequest request)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        if (student is null) return null;

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;

        var updated = await _studentRepository.UpdateStudentAsync(student);
        return updated is null ? null : StudentMapper.ToResponse(updated);
    }

    public async Task<bool> DeleteStudentAsync(string id)
    {
        return await _studentRepository.DeleteStudentAsync(id);
    }
}
