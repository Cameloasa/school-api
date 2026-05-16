namespace SchoolApi.Models.DTOs;

public class CourseInstanceResponse
{
    public string CourseInstanceId { get; set; } = default!;
    public string CourseId { get; set; } = default!;
    public string CourseTitle { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<StudentResponse> Students { get; set; } = new();
}
