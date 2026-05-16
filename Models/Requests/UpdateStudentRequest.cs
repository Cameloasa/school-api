using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models.Requests;

public struct UpdateStudentRequest
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
}
