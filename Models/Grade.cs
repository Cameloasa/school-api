//Models/Grade.cs
namespace SchoolApi.Models;

public class Grade
{
    
    public string GradeId { get; } = Guid.NewGuid().ToString();
    public string Value { get; set; }
    public CourseInstance CourseInstance { get; set; }
    public Student Student { get; set; }

    public Grade(string value, CourseInstance courseInstance, Student student)
    {
        Value = value;
        CourseInstance = courseInstance;
        Student = student;
    }
}