
using Microsoft.EntityFrameworkCore;
using SchoolApi.Context;
using SchoolApi.Models;
namespace SchoolApi.Repositories;

// =========================
//      INTERFACE
// =========================
public interface ICourseRepository
{
    Task<Course> AddCourseAsync(Course course);
    Task<Course?> GetCourseByIdAsync(string id);
    Task<List<Course>> GetCoursesAsync();
    Task<Course?> UpdateCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(string id);
}

// =========================
//   EF CORE Implementation
// =========================
public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // get all courses
    public async Task<List<Course>> GetCoursesAsync()
    {
        return await _context
        .Courses.ToListAsync();
    }

    // get course by id
    public async Task<Course?> GetCourseByIdAsync(string id)
    {
        return await _context
        .Courses
        .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    // add a new course
    public async Task<Course> AddCourseAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }
        

    // update course description
    public async Task<Course?> UpdateCourseAsync(Course course)
    {
        
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }

    // delete 
    public async Task<bool> DeleteCourseAsync(string id)
    {
        var course = await GetCourseByIdAsync(id);
        if(course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }   
}