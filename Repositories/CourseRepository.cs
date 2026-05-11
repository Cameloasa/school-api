
using SchoolApi.Models;
namespace SchoolApi.Repositories;

public interface ICourseRepository
{
    IEnumerable<Course> GetAllCourses();
    Course? GetCourseById(string id);
    bool AddCourse(Course course);
    Course? UpdateCourseDescription(string courseId, string newDescription);
    bool DeleteCourse(string id);
}
public class CourseRepository : ICourseRepository
{
    private List<Course> courses;

    public CourseRepository()
    {
        courses = [
            new ("Mathematics", "An introduction to mathematical concepts and techniques."),
            new ("Physics", "A study of the fundamental principles governing the natural world."),
            new ("Chemistry", "An exploration of the properties and interactions of matter."),
            new ("Biology", "An examination of living organisms and their interactions with the environment."),
            new ("Computer Science", "A comprehensive overview of computer systems and programming."),
            new("History", "A study of past events and civilizations."),
            new("Geography", "An exploration of Earth's landscapes, environments, and populations."),
            new("Philosophy", "An introduction to fundamental questions about existence, knowledge, and ethics."),
            new("Economics", "A study of production, consumption, and distribution of resources."),
            new("Literature", "An analysis of written works across different periods and cultures."),
            new("Statistics", "An introduction to data analysis, probability, and statistical methods."),
            new("Software Engineering", "Principles and practices of designing and building software systems."),
            new("Databases", "Fundamentals of database design, SQL, and data management."),
            new("Cybersecurity", "An overview of protecting systems, networks, and data from digital attacks.")
    ];
    }

    // get all courses
    public IEnumerable<Course> GetAllCourses()
    {
        return courses;
    }

    // get course by id
    public Course? GetCourseById(string id)
    {
        return courses.FirstOrDefault(c => c.CourseId == id);
    }

    // add a new course
    public bool AddCourse(Course course)
    {
        if (course == null) return false;
        
        courses.Add(course); return true;
    }

    // update course description
    public Course? UpdateCourseDescription(string courseId, string newDescription)
    {
        
        Course? existing = GetCourseById(courseId);
        if (existing == null)
        {
            return null;
        }

        existing.Description = newDescription;
        return existing;
        // save changes if using a real database context
    }

    // delete a course
    public bool DeleteCourse(string id)
    {
        var course = GetCourseById(id);
        if (course == null) return false;
        
        courses.Remove(course);
        return true;
    }   
}