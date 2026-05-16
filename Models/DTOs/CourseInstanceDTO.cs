namespace SchoolApi.Models.DTOs;

public struct CourseInstanceDTO
{
    public string CourseInstanceId { get; set; }

    public string CourseId { get; set; }
    public string CourseTitle { get; set; }

    public ICollection<StudentDTO> Students { get; set; }
}
