using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct CreateCourseRequest{

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
    public string Description { get; set; }
}