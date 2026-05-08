namespace SchoolApi.Models.Requests;

public struct CreateGradeRequest
{
    public string Value { get; set; }
    public string CourseInstanceId { get; set; }
    public string StudentId { get; set; }
}

public struct UpdateGradeRequest
{
    public string Value { get; set; }
}