using ECommerce_API.Services.Abstract;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
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

        //Initialize Seed
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister user)
        {
            var response = await _authService.Register(new Entities.User
            {
                Email = user.Email
            },user.Password);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin user)
        {
            var result = await _authService.Login(user.Email, user.Password);
            if (result == null)
            {
                return BadRequest("Fail");
            }
            return Ok(result);
        }


        [HttpPut("role-for-admin/{email}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> RoleAdmin([FromRoute]string email)
        {
            var result = await _authService.RoleForAdmin(email);
            return Ok(result);

        }
        [HttpPut("change-password")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword user)
        {
            var result = await _authService.ChangePassword(user.OldPassword, user.NewPassword, user.ConfirmPassword);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteAccount(string password)
        {
            var result = await _authService.DeleteAccount(password);
            return Ok(result);
        }


    }
}
