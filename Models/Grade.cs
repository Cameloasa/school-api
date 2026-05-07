//Models/Grade.cs
namespace SchoolApi.Models;

public class Grade
{
    private static int _counter = 1;
    
    public int GradeId { get; private set; }
    public string Value { get; set; }
    public CourseInstance CourseInstance { get; set; }
    public Student Student { get; set; }

    public Grade(string value, CourseInstance courseInstance, Student student)
    {
        GradeId = _counter++;
        Value = value;
        CourseInstance = courseInstance;
        Student = student;
    }
}