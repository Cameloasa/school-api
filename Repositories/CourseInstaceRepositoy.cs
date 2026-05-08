using SchoolApi.Models;
namespace SchoolApi.Repositories;
public interface ICourseInstanceRepository
{
    // CRUD operations
    bool AddCourseInstance(CourseInstance courseInstance);
    CourseInstance? GetCourseInstanceById(string id);
    IEnumerable<CourseInstance> GetAllCourseInstances();
    CourseInstance? UpdateCourseInstance(CourseInstance courseInstance);
    bool DeleteCourseInstance(string id);
    
    // Specialized searches - optimized for DB
    IEnumerable<CourseInstance> GetByStudentId(string studentId);      // WHERE StudentId = ?
    IEnumerable<CourseInstance> GetByCourseId(string courseId);        // WHERE CourseId = ?
    IEnumerable<CourseInstance> GetByDateRange(DateTime start, DateTime end); // BETWEEN
   
}

public class CourseInstanceRepository : ICourseInstanceRepository
{

    // In-memory storage for course instances
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
        var existing = GetCourseInstanceById(id);
        if (existing == null) return false;
        
        return courseInstances.Remove(existing);
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

    public IEnumerable<CourseInstance> GetByDateRange(DateTime start, DateTime end)
    {
        return courseInstances.Where(ci => ci.StartDate <= end && ci.EndDate >= start);
    }

    public IEnumerable<CourseInstance> GetByStudentId(string studentId)
    {
        return courseInstances.Where(ci => ci.Students.Any(s => s.StudentId == studentId));
    }

    public CourseInstance? GetCourseInstanceById(string id)
    {
        return courseInstances.FirstOrDefault(ci => ci.CourseInstanceId == id);
    }

    public CourseInstance? UpdateCourseInstance(CourseInstance courseInstance)
    {
        if (courseInstance == null)
        {
            return null;
        }
        var existing = GetCourseInstanceById(courseInstance.CourseInstanceId);
        if (existing == null)        {
            return null;
        }
        existing.EndDate = courseInstance.EndDate;
        existing.StartDate = courseInstance.StartDate;
        existing.Course = courseInstance.Course;
        existing.Students = courseInstance.Students;
        return existing;
    }
}