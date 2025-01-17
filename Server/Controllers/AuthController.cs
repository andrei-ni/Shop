﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrPrint.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(new Shared.User
            {
                Email = request.Email,
                VerificationToken = request.VerificationToken
            }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), newPassword);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("verify")]
        public async Task<ActionResult<ServiceResponse<string>>> Verify(string token)
        {
            var response = await _authService.Verify(token);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ResetPassword(UserResetPassword request)
        {
            var response = await _authService.ResetPassword(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpDelete("{email}")] // Authorize(Roles = "Customer")
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(string email)
        {
            var result = await _authService.DeleteUser(email);
            return Ok(result);
        }

        [HttpGet("users/{page}")] //, Authorize(Roles = "Admin")
        public async Task<ActionResult<UserResponseDTO>> GetUsers(int page = 1)
        {
            var result = await _authService.GetUsers(page);
            return Ok(result);

        }
    }
}
