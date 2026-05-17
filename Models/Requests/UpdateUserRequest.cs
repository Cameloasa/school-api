namespace SchoolApi.Models.Requests;
public class UpdateUserRequest
{
    public string? Password { get; set; } 
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
}