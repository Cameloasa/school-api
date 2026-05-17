
using SchoolApi.Mappers;
using SchoolApi.Models;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

namespace SchoolApi.Services;

// =========================
//      INTERFACE
// =========================

public interface IGradeService
{
    Task<List<GradeResponse>> GetGradesAsync();
    Task<GradeResponse?> GetGradeByIdAsync(string id);
    Task<List<GradeResponse>> GetGradesByStudentIdAsync(string studentId);
    Task<List<GradeResponse>> GetGradesByCourseInstanceIdAsync(string courseInstanceId);

    Task<GradeResponse> CreateGradeAsync(CreateGradeRequest request);
    Task<GradeResponse?> UpdateGradeAsync(string id, UpdateGradeRequest request);
    Task<bool> DeleteGradeAsync(string id);
}
// =========================
//      IMPLEMENTATION
// =========================
public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepo;
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseInstanceRepository _courseInstanceRepo;

    public GradeService(
        IGradeRepository gradeRepo,
        IStudentRepository studentRepo,
        ICourseInstanceRepository courseInstanceRepo)
    {
        _gradeRepo = gradeRepo;
        _studentRepo = studentRepo;
        _courseInstanceRepo = courseInstanceRepo;
    }

    // GET ALL
    public async Task<List<GradeResponse>> GetGradesAsync()
    {
        var grades = await _gradeRepo.GetGradesAsync();
        return grades.Select(GradeMapper.ToResponse).ToList();
    }

    // GET BY ID
    public async Task<GradeResponse?> GetGradeByIdAsync(string id)
    {
        var grade = await _gradeRepo.GetGradeByIdAsync(id);
        return grade is null ? null : GradeMapper.ToResponse(grade);
    }

    // GET BY STUDENT
    public async Task<List<GradeResponse>> GetGradesByStudentIdAsync(string studentId)
    {
        var grades = await _gradeRepo.GetGradesByStudentIdAsync(studentId);
        return grades.Select(GradeMapper.ToResponse).ToList();
    }

    // GET BY COURSE INSTANCE
    public async Task<List<GradeResponse>> GetGradesByCourseInstanceIdAsync(string courseInstanceId)
    {
        var grades = await _gradeRepo.GetGradesByCourseInstanceIdAsync(courseInstanceId);
        return grades.Select(GradeMapper.ToResponse).ToList();
    }

    // CREATE
    public async Task<GradeResponse> CreateGradeAsync(CreateGradeRequest request)
    {
        // 1. Validate student
        var student = await _studentRepo.GetStudentByIdAsync(request.StudentId);
        if (student == null)
            throw new Exception("Student not found");

        // 2. Validate course instance
        var instance = await _courseInstanceRepo.GetInstanceByIdAsync(request.CourseInstanceId);
        if (instance == null)
            throw new Exception("Course instance not found");

        // 3. Create grade
        var grade = new Grade
        {
            Value = request.Value,
            StudentId = request.StudentId,
            CourseInstanceId = request.CourseInstanceId
        };

        var created = await _gradeRepo.AddGradeAsync(grade);

        return GradeMapper.ToResponse(created);
    }

    // UPDATE
    public async Task<GradeResponse?> UpdateGradeAsync(string id, UpdateGradeRequest request)
    {
        var existing = await _gradeRepo.GetGradeByIdAsync(id);
        if (existing == null)
            return null;

        existing.Value = request.Value;

        var updated = await _gradeRepo.UpdateGradeAsync(existing);

        return GradeMapper.ToResponse(updated!);
    }

    // DELETE
    public async Task<bool> DeleteGradeAsync(string id)
    {
        return await _gradeRepo.DeleteGradeAsync(id);
    }
}