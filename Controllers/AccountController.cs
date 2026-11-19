using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth_api.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using auth_api.Models;
using auth_api.Interfaces;


namespace auth_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new AppUser
            {
                UserName = registerDto.UserName,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var role = await userManager.AddToRoleAsync(user, "Member");
                if (role.Succeeded)
                {
                    return Ok(new { result = "User created successfully" });
                }
                return BadRequest(new { result = "User created but role assignment failed" });
            }

            return BadRequest(new { result = "User creation failed", errors = result.Errors.Select(e => e.Description) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(loginDto.Username!);
            if (user == null)
            {
                return Unauthorized(new { result = "Invalid username or password" });
            }

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password!);
            if (result)
            {
                return Ok(new LoggedInUserDto { UserName = user.UserName!, Token = tokenService.CreateToken(user) });
            }

            return Unauthorized(new { result = "Invalid username or password" });
        }
    }

}