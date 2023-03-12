using ECommerce_API.DataContext;
using ECommerce_API.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        private readonly IBasketService _basketService;
        private readonly IUserMoneyService _userMoneyService;
        private readonly IEmailSender _emailSender;
        public OrderService(IAuthService authService
                            ,ApplicationDbContext context
                            , IBasketService basketService,
                            IUserMoneyService userMoneyService,
                            IEmailSender emailSender)
        {
            _authService = authService;
            _basketService = basketService;
            _context = context;
            _userMoneyService = userMoneyService;
            _emailSender = emailSender;
        }

        public async Task<ServiceResponse<List<Order>>> GetOrderByUser()
        {
            var user = _authService.GetUserId();
            var result = await _context.Orders.Where(x => x.UserId == user).ToListAsync();
            if (result == null)
            {
                return new ServiceResponse<List<Order>>
                {
                    Success = false,
                    Message = "You dont have any Order"
                };
            }
            else
            {
                return new ServiceResponse<List<Order>>
                {
                    Data = result,
                    Success = true,
                };
            }
        }

        public async Task<ServiceResponse<bool>> Refund(int orderId)
        {
            var user = _authService.GetUserId();
            var AnyOrder = await _context.Orders.AnyAsync(x=>x.UserId== user);
            var response = await _context.Orders.FirstOrDefaultAsync(x=>x.Id== orderId);
            var userMoney = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == user);

            if (AnyOrder == true)
            {
                response.Status = false;
                _context.Orders.Update(response);
                await _context.SaveChangesAsync();
            }
            if (response.Status == false)
            {
                userMoney.Money = userMoney.Money + response.TotalPrice;
                _context.UserMoneys.Update(userMoney);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = "Your chip money return back",
                };
            }
            return new ServiceResponse<bool>
            {
                Success = false,
            };
        }

        public async Task<ServiceResponse<List<Order>>> StoreCartItem(List<Order> order)
        {
            var user = _authService.GetUserId();
            var result = await _context.Baskets.Where(x => x.UserId ==user).ToListAsync();
            var checkMoney = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == user);
            var User = await _context.Users.FirstOrDefaultAsync(x => x.Id == user);
            var orders = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == user);

            decimal count = 0;
            if (result == null)
            {
                return new ServiceResponse<List<Order>>
                {
                    Message = "You dont have any basket item",
                    Success = false,
                };
            }
            foreach (var item in result)
            {
                count = count + item.TotalPrice;
            }
            if (checkMoney.Money<count)
            {
                return new ServiceResponse<List<Order>>
                {
                    Success = false,
                    Message = "Your money is not equals this products,"
                };
            }
            foreach (var item in result)
            {
                Order deneme = new Order();
                deneme.ProductName = item.ProductName;
                deneme.UserId = item.UserId;
                deneme.ProductId = item.ProductId;
                deneme.ProductPrice = item.Price;
                deneme.Status = true;
                deneme.TotalPrice = item.TotalPrice;
                order.Add(deneme);
                await _basketService.DeleteBasket(item);
            }
            _context.Orders.AddRange(order);
            await _userMoneyService.DeleteMoney(count);
            await _context.SaveChangesAsync();
            await _emailSender.SendEmailAsync(User.Email , User.Role , " :  Order is success");

            return new ServiceResponse<List<Order>>
            {
                Success = true,
                Message = "Your Order Is Successfully",
            };

        }
    }
}
