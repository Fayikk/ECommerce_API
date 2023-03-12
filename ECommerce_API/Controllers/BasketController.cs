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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [HttpPost,Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateBasket(Basket basket)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return BadRequest("User is not found");
                
            }
            basket.UserId = int.Parse(user);
            var result = await _basketService.AddBasket(basket);
            return Ok(result);


        }
    }
}
