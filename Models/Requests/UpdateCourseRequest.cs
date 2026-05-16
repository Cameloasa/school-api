using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct UpdateCourseRequest
{
    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 250 characters")]
    public string Description { get; set; }
}
