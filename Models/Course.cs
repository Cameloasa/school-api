namespace SchoolApi.Models;

public class Course
{
    public string CourseId { get; set; } = Guid.NewGuid().ToString()[..6];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<CourseInstance> Instances { get; set; } = new List<CourseInstance>();

    // EF Core needs only this:
    public Course() {}
}
