namespace SchoolApi.Models.Requests;

public struct CreateCourseInstancesRequest{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CourseId { get; set; }
    public List<int> StudentId { get; set; }
}