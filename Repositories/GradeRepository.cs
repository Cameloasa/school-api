
using SchoolApi.Models;

namespace SchoolApi.Repositories;

// =========================
//      INTERFACE
// =========================
public interface IGradeRepository
{
    Task<List<Grade>> GetGradesAsync();
    Task<Grade?> GetGradeByIdAsync(string id);
    Task<List<Grade>> GetGradesByStudentIdAsync(string studentId);
    Task<List<Grade>> GetGradesByCourseInstanceIdAsync(string courseInstanceId);

    Task<Grade> AddGradeAsync(Grade grade);
    Task<Grade?> UpdateGradeAsync(Grade grade);
    Task<bool> DeleteGradeAsync(string id);
}
// =========================
//   EF CORE Implementation
// =========================
public class GradeRepository : IGradeRepository
{
    public Task<Grade> AddGradeAsync(Grade grade)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteGradeAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Grade?> GetGradeByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Grade>> GetGradesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Grade>> GetGradesByCourseInstanceIdAsync(string courseInstanceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Grade>> GetGradesByStudentIdAsync(string studentId)
    {
        throw new NotImplementedException();
    }

    public Task<Grade?> UpdateGradeAsync(Grade grade)
    {
        throw new NotImplementedException();
    }
}



