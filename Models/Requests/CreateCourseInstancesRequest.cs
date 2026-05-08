namespace SchoolApi.Models.Requests;

public struct CreateCourseInstancesRequest{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CourseId { get; set; }
    public List<string> StudentId { get; set; }
}