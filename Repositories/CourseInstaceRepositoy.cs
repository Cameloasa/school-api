using Microsoft.EntityFrameworkCore;
using SchoolApi.Context;
using SchoolApi.Models;

namespace SchoolApi.Repositories;

// =========================
//      INTERFACE
// =========================
public interface ICourseInstanceRepository
{
    Task<List<CourseInstance>> GetInstancesAsync();
    Task<CourseInstance?> GetInstanceByIdAsync(string id);
    Task<CourseInstance> AddInstanceAsync(CourseInstance instance);
    Task<CourseInstance?> UpdateInstanceAsync(CourseInstance instance);
    Task<bool> DeleteInstanceAsync(string id);

    // Extra (util):
    Task<List<CourseInstance>> GetInstancesByCourseIdAsync(string courseId);
    Task<CourseInstance?> GetByCourseAndDatesAsync(
    string courseId,
    DateTime start,
    DateTime end);
}
// =========================
//   EF CORE Implementation
// =========================
public class CourseInstanceRepository : ICourseInstanceRepository
{

    private readonly ApplicationDbContext _context;

    public CourseInstanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    //create
    public async Task<CourseInstance> AddInstanceAsync(CourseInstance instance)
    {
         _context.CourseInstances.Add(instance);
        await _context.SaveChangesAsync();
        return instance;
    }

    //delete
    public async Task<bool> DeleteInstanceAsync(string id)
    {
        var instance = await GetInstanceByIdAsync(id);
        if(instance == null)
            return false;

        _context.CourseInstances.Remove(instance);
        await _context.SaveChangesAsync();
        return true;
    }

    //get by id
    public async Task<CourseInstance?> GetInstanceByIdAsync(string id)
    {
        return await 
            _context
            .CourseInstances
            .Include(ci => ci.Course)
            .Include(ci => ci.Students)
            .Include(ci => ci.Grades)
            .FirstOrDefaultAsync(ci => ci.CourseInstanceId == id);
    }

    //get all
    public async Task<List<CourseInstance>> GetInstancesAsync()
    {
        return await 
            _context
            .CourseInstances
            .Include(ci => ci.Course)
            .Include(ci => ci.Students)
            .Include(ci => ci.Grades)
            .ToListAsync();
    }

    //get by course id
    public async Task<List<CourseInstance>> GetInstancesByCourseIdAsync(string courseId)
    {
        return await 
            _context
            .CourseInstances
            .Include(ci => ci.Course)
            .Include(ci => ci.Students)
            .Include(ci => ci.Grades)
            .Where(ci => ci.CourseId == courseId)
            .ToListAsync();

    }

    //update 
    public async Task<CourseInstance?> UpdateInstanceAsync(CourseInstance instance)
    {
        _context.CourseInstances.Update(instance);
        await _context.SaveChangesAsync();
        return instance;
    }

    public async Task<CourseInstance?> GetByCourseAndDatesAsync(
    string courseId,
    DateTime start,
    DateTime end)
{
    return await _context.CourseInstances
        .FirstOrDefaultAsync(ci =>
            ci.CourseId == courseId &&
            ci.StartDate == start &&
            ci.EndDate == end
        );
}
}

