namespace SchoolApi.Models.Requests;

public struct CreateGradeRequest
{
    public string CourseInstanceId { get; set; }
    public List<GradeEntry> Grades { get; set; }
}

public struct GradeEntry
{
    public string StudentId { get; set; }
    public string Value { get; set; }
}

public struct UpdateGradeRequest
{
    public string Value { get; set; }
}