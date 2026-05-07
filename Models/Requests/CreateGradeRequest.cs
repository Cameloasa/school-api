namespace SchoolApi.Models.Requests;

public class CreateGradeRequest
{
    public required string Value { get; set; }
    public int CourseInstanceId { get; set; }
    public int StudentId { get; set; }
}

public class UpdateGradeRequest
{
    public required string Value { get; set; }
}