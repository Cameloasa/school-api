using Microsoft.AspNetCore.Identity;
using SchoolApi.Models;
using SchoolApi.Models.Requests;
using SchoolApi.Repositories;

namespace SchoolApi.Services;
public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);
    Task<bool> ValidateUserAsync(LoginRequest request);
}
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IStudentRepository _studentRepo;

    public AuthService(UserManager<User> userManager, IStudentRepository studentRepo)
    {
        _userManager = userManager;
        _studentRepo = studentRepo;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var existing = await _userManager.FindByEmailAsync(request.Email);
        if (existing != null)
            throw new Exception("A user with this email already exists");

        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));

        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserId = user.Id
        };

        await _studentRepo.AddStudentAsync(student);
    }

    public async Task<bool> ValidateUserAsync(LoginRequest request)
{
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null)
        return false;

    return await _userManager.CheckPasswordAsync(user, request.Password);
}
}
