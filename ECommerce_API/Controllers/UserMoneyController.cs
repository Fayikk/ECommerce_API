using ECommerce_API.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMoneyController : ControllerBase
    {
        private readonly IUserMoneyService _userMoneyService;
        public UserMoneyController(IUserMoneyService userMoneyService)
        {
            _userMoneyService = userMoneyService;
        }


        [HttpPost,Authorize]
        public async Task<ActionResult<ServiceResponse<UserMoney>>> CreateUserMoney(UserMoney money)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user != null)
            {
                money.UserId = int.Parse(user);
                var response = await _userMoneyService.AddMoney(money);
                return Ok(money);
            }
            return BadRequest("Fail");
        }

    }
}
