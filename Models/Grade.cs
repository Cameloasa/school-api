//Models/Grade.cs
namespace SchoolApi.Models;

public class Grade
{
    
    public string GradeId { get; set; } 
    public string Value { get; set; }
    public CourseInstance CourseInstance { get; set; }
    public Student Student { get; set; }

    // Default constructor for deserialization
    public Grade()
    {
        GradeId = Guid.NewGuid().ToString()[..6];
        Value = string.Empty;
        CourseInstance = new CourseInstance();
        Student = new Student();
    }

    public Grade(string value, CourseInstance courseInstance, Student student)
    {
        GradeId = Guid.NewGuid().ToString();
        Value = value;
        CourseInstance = courseInstance;
        Student = student;
    }

    public Grade(string id, string value, CourseInstance courseInstance, Student student)
{
    GradeId = id;
    Value = value;
    CourseInstance = courseInstance;
    Student = student;
}
}