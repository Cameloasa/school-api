
using Microsoft.AspNetCore.Identity;

namespace SchoolApi.Models;
public class User:IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public string FullName => $"{FirstName} {LastName}";
}