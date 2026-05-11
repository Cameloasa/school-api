using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct CreateCourseRequest{

    [Required(ErrorMessage = "Title is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 250 characters")]
    public string Description { get; set; }
}

public struct UpdateCourseRequest{

    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 250 characters")]
    public string Description { get; set; }
}