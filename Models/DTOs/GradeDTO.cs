namespace SchoolApi.Models.DTOs;

public struct GradeDTO
{
    public string GradeId { get; set; }
    public string Value { get; set; }

    public string StudentId { get; set; }
    public string StudentFullName { get; set; }

    public string CourseInstanceId { get; set; }
    public string CourseTitle { get; set; }
}
