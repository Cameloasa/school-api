namespace SchoolApi.Models.Requests;

public struct CreateCourseRequest{
    public string Name { get; set; }
    public string Description { get; set; }
}