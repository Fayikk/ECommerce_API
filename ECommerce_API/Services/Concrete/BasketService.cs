using ECommerce_API.DataContext;
using ECommerce_API.Services.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public BasketService(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Basket>> AddBasket(Basket basket)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == basket.ProductId);

            var userId = _authService.GetUserId();
            if (product == null || userId == null)
            {
                return new ServiceResponse<Basket>
                {
                    Success = false,
                    Message = "This process is fail",
                };
            }
            else if (product != null && userId != null)
            {
                basket.ProductId = product.Id;
                basket.ProductName = product.ProductName;
                basket.Price = product.Price;
                basket.UserId = userId;
                basket.TotalPrice = product.Price * basket.Quantiy;
                _context.Baskets.Add(basket);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Basket>
                {
                    Data = basket,
                    Success = true,
                    Message = "Products added your basket is succeseed",
                };
            }

            return null;
        }

        public async Task<ServiceResponse<Basket>> DeleteBasket(Basket basket)
        {
            var result = await _context.Baskets.FirstOrDefaultAsync(x => x.Id == basket.Id);
            if (result == null)
            {
                return new ServiceResponse<Basket>
                {
                    Success = false,
                };
            }
            else
            {
                _context.Baskets.Remove(result);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Basket>
                {
                    Success = true,
                    Data = result,
                };
            }

        }
    }
}
