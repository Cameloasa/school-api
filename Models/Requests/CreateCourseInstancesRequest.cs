using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct CreateCourseInstancesRequest{

    [Required(ErrorMessage = "StartDate is required and cannot be in the past")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "EndDate is required and must be later than StartDate")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "CourseId is required")]
    [MinLength(1, ErrorMessage = "CourseId cannot be empty")]
    public string CourseId { get; set; }

    [Required(ErrorMessage = "StudentIds is required")]
    [MinLength(1, ErrorMessage = "At least one StudentId is required")]
    public List<string> StudentIds { get; set; }
}

public struct UpdateCourseInstanceRequest
{
    [Required(ErrorMessage = "StartDate is required and cannot be in the past")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "EndDate is required and must be later than StartDate")]
    public DateTime EndDate { get; set; }
}