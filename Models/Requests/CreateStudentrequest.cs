//models/Requests/CreateStudentRequest.cs

using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests
{
    public struct CreateStudentRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email must be a valid email address")]
        public string Email { get; set; }
    }
}
