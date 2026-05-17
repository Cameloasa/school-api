
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
    public Task<GradeResponse> CreateGradeAsync(CreateGradeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteGradeAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<GradeResponse?> GetGradeByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GradeResponse>> GetGradesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<GradeResponse>> GetGradesByCourseInstanceIdAsync(string courseInstanceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<GradeResponse>> GetGradesByStudentIdAsync(string studentId)
    {
        throw new NotImplementedException();
    }

    public Task<GradeResponse?> UpdateGradeAsync(string id, UpdateGradeRequest request)
    {
        throw new NotImplementedException();
    }
}