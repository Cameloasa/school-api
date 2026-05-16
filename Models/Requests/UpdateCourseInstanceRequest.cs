using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct UpdateCourseInstanceRequest
{
    [Required(ErrorMessage = "StartDate is required and cannot be in the past")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "EndDate is required and must be later than StartDate")]
    public DateTime EndDate { get; set; }
}
