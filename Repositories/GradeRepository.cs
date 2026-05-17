using Microsoft.EntityFrameworkCore;
using SchoolApi.Context;
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
    private readonly ApplicationDbContext _context;

    public GradeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL
    public async Task<List<Grade>> GetGradesAsync()
    {
        return await _context
                .Grades
                .Include(g => g.Student)
                .Include(g => g.CourseInstance)
                .ThenInclude(ci => ci.Course)
                .ToListAsync();
    }

    // GET BY ID
    public async Task<Grade?> GetGradeByIdAsync(string id)
    {
        return await _context
                .Grades
                .Include(g => g.Student)
                .Include(g => g.CourseInstance)
                .ThenInclude(ci => ci.Course)
                .FirstOrDefaultAsync(g => g.GradeId == id);
    }

    // GET BY STUDENT
    public async Task<List<Grade>> GetGradesByStudentIdAsync(string studentId)
    {
        return await _context.Grades
            .Where(g => g.StudentId == studentId)
            .Include(g => g.Student)
            .Include(g => g.CourseInstance)
                .ThenInclude(ci => ci.Course)
            .ToListAsync();
    }

    // GET BY COURSE INSTANCE
    public async Task<List<Grade>> GetGradesByCourseInstanceIdAsync(string courseInstanceId)
    {
        return await _context.Grades
            .Where(g => g.CourseInstanceId == courseInstanceId)
            .Include(g => g.Student)
            .Include(g => g.CourseInstance)
                .ThenInclude(ci => ci.Course)
            .ToListAsync();
    }

    // CREATE
    public async Task<Grade> AddGradeAsync(Grade grade)
    {
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    // UPDATE
    public async Task<Grade?> UpdateGradeAsync(Grade grade)
    {
        _context.Grades.Update(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    // DELETE
    public async Task<bool> DeleteGradeAsync(string id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null)
            return false;

        _context.Grades.Remove(grade);
        await _context.SaveChangesAsync();
        return true;
    }
}



