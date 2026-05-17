using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public class CreateGradeRequest
{
    [Required(ErrorMessage = "StudentId is required")]
    public string StudentId { get; set; } = default!;

    [Required(ErrorMessage = "CourseInstanceId is required")]
    public string CourseInstanceId { get; set; } = default!;

    [Required(ErrorMessage = "Grade value is required")]
    [MinLength(1, ErrorMessage = "Grade value cannot be empty")]
    public string Value { get; set; } = default!;
}
