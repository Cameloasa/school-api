//Services/CourseService.cs
namespace SchoolApi.Services;
using SchoolApi.Models;
using SchoolApi.Models.Requests;

public interface ICourseService
{
    List<Course> GetCourses();
    Course? GetCourseById(int id);
    Course CreateCourse(CreateCourseRequest request);
    Course? UpdateCourse(int id, CreateCourseRequest request);
    bool DeleteCourse(int id);
}

public class CourseService : ICourseService
{
    // List of courses
List<Course> courses = [
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

    public List<Course> GetCourses()
    {
        return courses;
    }

    public Course? GetCourseById(int id)
    {
        Course? found = courses.FirstOrDefault(c => c.CourseId == id);
        return found;
    }

    public Course CreateCourse(CreateCourseRequest request)
    {
        Course newCourse = new(request.Description, request.Name);
        courses.Add(newCourse);
        return newCourse;
    }

    public Course? UpdateCourse(int id, CreateCourseRequest request)
    {
        Course? found = courses.FirstOrDefault(c => c.CourseId == id);
        if(found == null)
        {
            return null;
        }

        found.Name = request.Name;
        found.Description = request.Description;
        return found;
    }

    public bool DeleteCourse(int id)
    {
        Course? found = courses.FirstOrDefault(c => c.CourseId == id);
        if(found == null)
        {
            return false;
        }
        courses.Remove(found);
        return true;
    }
}