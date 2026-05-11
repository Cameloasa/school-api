using SchoolApi.Models;
using SchoolApi.Models.Requests;
namespace SchoolApi.Repositories;
public interface ICourseInstanceRepository
{
    // CRUD operations
    bool AddCourseInstance(CourseInstance courseInstance);
    CourseInstance? GetCourseInstanceById(string id);
    IEnumerable<CourseInstance> GetAllCourseInstances();
    CourseInstance? UpdateCourseInstanceDate(string courseInstanceId, DateTime newStartDate, DateTime newEndDate);
    bool DeleteCourseInstance(string id);
    
    // Specialized searches - optimized for DB
    IEnumerable<CourseInstance> GetByStudentId(string studentId);      // WHERE StudentId = ?
    IEnumerable<CourseInstance> GetByCourseId(string courseId);        // WHERE CourseId = ?
    IEnumerable<CourseInstance> GetByDateRange(DateTime startDate, DateTime endDate); // BETWEEN
   
}

public class CourseInstanceRepository : ICourseInstanceRepository
{

    // In-memory list to store course instances
    private List<CourseInstance> courseInstances;

    // Constructor to initialize the repository with some sample data
    public CourseInstanceRepository()
    {
        courseInstances = [];
    }

    // Add a new course instance
    public bool AddCourseInstance(CourseInstance courseInstance)
    {
        if (courseInstance == null)return false;

        courseInstances.Add(courseInstance); return true;
    }

    // Delete a course instance by ID
    public bool DeleteCourseInstance(string id)
    {
        var courseInstance = GetCourseInstanceById(id);
        if (courseInstance == null) return false;
        
        return courseInstances.Remove(courseInstance);
    }

    // Get all course instances
    public IEnumerable<CourseInstance> GetAllCourseInstances()
    {
        return courseInstances;
    }

    // Get course instances by course ID
    public IEnumerable<CourseInstance> GetByCourseId(string courseId)
    {
        return courseInstances.Where(ci => ci.Course?.CourseId == courseId);

    }

    // Get course instances by date range
    public IEnumerable<CourseInstance> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        return courseInstances.Where(ci => ci.StartDate <= endDate && ci.EndDate >= startDate);
    }

    // Get course instances by student ID
    public IEnumerable<CourseInstance> GetByStudentId(string studentId)
    {
        return courseInstances.Where(ci => ci.Students.Any(s => s.StudentId == studentId));
    }

    // Get a course instance by ID
    public CourseInstance? GetCourseInstanceById(string id)
    {
        return courseInstances.FirstOrDefault(ci => ci.CourseInstanceId == id);
    }

    // Update a course instance
    public CourseInstance? UpdateCourseInstanceDate(string id, DateTime newStartDate, DateTime newEndDate)
    {
        CourseInstance? existing = GetCourseInstanceById(id);
        if (existing == null) return null;
        
        existing.StartDate = newStartDate;
        existing.EndDate = newEndDate;
        return existing;
        // save changes if using a real database context
    }
}