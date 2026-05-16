using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct CreateCourseInstanceRequest
{
    [Required(ErrorMessage = "StartDate is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "EndDate is required")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "CourseId is required")]
    public string CourseId { get; set; }

    [Required(ErrorMessage = "StudentIds is required")]
    [MinLength(1, ErrorMessage = "At least one StudentId is required")]
    public List<string> StudentIds { get; set; }
}
