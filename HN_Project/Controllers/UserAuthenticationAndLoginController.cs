using Azure.Core;
using HN_Backend.DTOs;
using HN_Backend.Service;
using HN_Project.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HN_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationAndLoginController : ControllerBase
    {
        private readonly UserAuthenticationAndLoginService _userAuthenticationAndLoginService;
        public UserAuthenticationAndLoginController(UserAuthenticationAndLoginService userAuthenticationAndLoginService)
        {
            _userAuthenticationAndLoginService = userAuthenticationAndLoginService;
        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> SaveLoginUser([FromForm] LoginUserEntryVM request)
        {
            if (request.FirstPassword != request.ConfirmPassword)
            {
                return BadRequest(new
                {
                    message = "Password and Confirm Password do not match."
                });
            }

            var existingUser = await _userAuthenticationAndLoginService.GetUserByUserNameEmailAndPhoneAsync(request.Phone);

            if (existingUser != null)
            {
                return BadRequest(new
                {
                    message = "This phone number is already registered."
                });
            }

            var existingUserEmail = await _userAuthenticationAndLoginService.GetUserByUserNameEmailAndPhoneAsync(request.Email);

            if (existingUserEmail != null)
            {
                return BadRequest(new
                {
                    message = "This Email is already registered."
                });
            }
            var user = await _userAuthenticationAndLoginService.SaveUser(request);

            return Ok(user);
        }

        [HttpPost("LoginUserByPassword")]
        public async Task<IActionResult> LoginUserAsync(LoginRequestDto dto)
        {
            var user = await _userAuthenticationAndLoginService.LoginUserAsync(dto.ParamObj, dto.Password);
            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid phone/email or password."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Login successful.",
                data = user
            });
        }
    }
}
