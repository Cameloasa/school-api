using Microsoft.EntityFrameworkCore;
using SchoolApi.Context;
using SchoolApi.Models;

namespace SchoolApi.Repositories;

// =========================
//      INTERFACE
// =========================
public interface IStudentRepository
{
    Task<Student> AddStudentAsync(Student student);
    Task<Student?> GetStudentByIdAsync(string id);
    Task<List<Student>> GetStudentsAsync();
    Task<Student?> UpdateStudentAsync(Student student);
    Task<bool> DeleteStudentAsync(string id);
}

// =========================
//   EF CORE Implementation
// =========================
public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Student> AddStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student?> GetStudentByIdAsync(string id)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.StudentId == id);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student?> UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<bool> DeleteStudentAsync(string id)
    {
        var student = await GetStudentByIdAsync(id);
        if (student == null)
            return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }

}
