using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct UpdateGradeRequest
{
    [Required(ErrorMessage = "Grade value is required")]
    [MinLength(1, ErrorMessage = "Grade value cannot be empty")]
    public string Value { get; set; }
}
