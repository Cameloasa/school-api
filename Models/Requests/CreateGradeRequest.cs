using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct CreateGradeRequest
{
    [Required(ErrorMessage = "Course instance ID is required")]
    [MinLength(1, ErrorMessage = "Course instance ID cannot be empty")]
    public string CourseInstanceId { get; set; }

    [Required(ErrorMessage = "Grades are required")]
    [MinLength(1, ErrorMessage = "At least one grade is required")]
    public List<GradeEntry> Grades { get; set; }
}

public struct GradeEntry
{
    [Required(ErrorMessage = "Student ID is required")]
    [MinLength(1, ErrorMessage = "Student ID cannot be empty")]
    public string StudentId { get; set; }
    [Required(ErrorMessage = "Grade value is required")]
    [MinLength(1, ErrorMessage = "Grade value cannot be empty")]
    public string Value { get; set; }
}

public struct UpdateGradeRequest
{
    [Required(ErrorMessage = "Grade value is required")]
    [MinLength(1, ErrorMessage = "Grade value cannot be empty")]
    public string Value { get; set; }
}