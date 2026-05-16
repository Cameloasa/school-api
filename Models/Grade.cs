
namespace SchoolApi.Models;

public class Grade
{
    public string GradeId { get; set; } = Guid.NewGuid().ToString()[..6];
    public string Value { get; set; } = string.Empty;

    public string StudentId { get; set; } = string.Empty;
    public Student Student { get; set; }

    public string CourseInstanceId { get; set; } = string.Empty;
    public CourseInstance CourseInstance { get; set; }

    // EF Core needs only this:
    public Grade() {}
}
