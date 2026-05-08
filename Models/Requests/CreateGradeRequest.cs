namespace SchoolApi.Models.Requests;

public class CreateGradeRequest
{
    public required string Value { get; set; }
    public string CourseInstanceId { get; set; }
    public string StudentId { get; set; }
}

public class UpdateGradeRequest
{
    public required string Value { get; set; }
}