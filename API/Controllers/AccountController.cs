using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController(SignInManager<User> signInManager) : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName
            };
            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded) return Ok();
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem();
        }
        
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated) return NoContent();
            var user = await signInManager.UserManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            return Ok(new
            {
                user.DisplayName,
                user.Id,
                user.Email,
                user.ImageUrl
            });
        }
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
    }
}