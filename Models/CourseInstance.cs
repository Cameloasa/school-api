namespace SchoolApi.Models;

public class CourseInstance
{
    public string CourseInstanceId { get; set; } = Guid.NewGuid().ToString()[..6];

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string CourseId { get; set; } = string.Empty;
    public Course Course { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    // EF Core needs only this:
    public CourseInstance() {}
}
