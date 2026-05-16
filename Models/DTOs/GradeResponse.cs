namespace SchoolApi.Models.DTOs;

public class GradeResponse
{
    public string GradeId { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string StudentId { get; set; } = default!;
    public string CourseInstanceId { get; set; } = default!;
}
