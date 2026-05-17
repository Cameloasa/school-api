
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Models.DTOs;
using SchoolApi.Models.Requests;
namespace SchoolApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager){
        _userManager = userManager;
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
            return Unauthorized();

        // Update FirstName / LastName 
        if (!string.IsNullOrWhiteSpace(request.FirstName))
            currentUser.FirstName = request.FirstName;

        if (!string.IsNullOrWhiteSpace(request.LastName))
            currentUser.LastName = request.LastName;

        // Save
        var updateResult = await _userManager.UpdateAsync(currentUser);
        if (!updateResult.Succeeded)
            return BadRequest(updateResult.Errors);

        // Changing password
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
            var passResult = await _userManager.ResetPasswordAsync(currentUser, token, request.Password);

            if (!passResult.Succeeded)
                return BadRequest(passResult.Errors);
        }

        return Ok("User updated successfully");
    }

    [HttpGet("me")]
[Authorize]
public async Task<ActionResult<UserProfileResponse>> GetCurrentUser()
{
    var currentUser = await _userManager.GetUserAsync(User);

    if (currentUser == null)
        return Unauthorized();

    var response = new UserProfileResponse
    {
        Email = currentUser.Email!,
        FirstName = currentUser.FirstName,
        LastName = currentUser.LastName,
        FullName = currentUser.FullName
    };

    return Ok(response);
}
}
