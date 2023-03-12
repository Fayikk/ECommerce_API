using ECommerce_API.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }



        [HttpGet,Authorize]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> GetOrderByUser()
        {
            var result = await _orderService.GetOrderByUser();
            return Ok(result);
        }


        [HttpPost,Authorize]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> StoreCartItems(List<Order> order)
        {
            var result = await _orderService.StoreCartItem(order);
            return Ok(result);
        }

        [HttpPost("{orderId}"),Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> Refund([FromRoute]int orderId)
        {
            var result = await _orderService.Refund(orderId);
            return Ok(result);
        }
    }
}
