
namespace SchoolApi.Models;

public class Student
{
    public string StudentId { get; set; } = Guid.NewGuid().ToString()[..6];

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public ICollection<CourseInstance> CourseInstances { get; set; } = new List<CourseInstance>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    // EF Core needs only this:
    public Student() {}
}
