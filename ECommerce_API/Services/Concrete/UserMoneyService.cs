using ECommerce_API.DataContext;
using ECommerce_API.Services.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Services.Concrete
{
    public class UserMoneyService : IUserMoneyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public UserMoneyService(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<UserMoney>> AddMoney(UserMoney userMoney)
        {
            var user = _authService.GetUserId();
            var result = await _context.UserMoneys.FirstOrDefaultAsync(x=>x.UserId==userMoney.UserId);


            if (result != null)
            {
                result.Money += userMoney.Money;
                _context.UserMoneys.Update(result);
                await _context.SaveChangesAsync();
                return new ServiceResponse<UserMoney>
                {
                    Message = $"Your chip money is =  {result.Money}",
                    Success = true,
                };
            }
            else
            {
                _context.UserMoneys.Add(userMoney);
                await _context.SaveChangesAsync();
                return new ServiceResponse<UserMoney>
                {
                    Message = $"Your chip money is =  {userMoney.Money}",
                    Success = true,
                };
            }
        }

        public async Task<ServiceResponse<UserMoney>> DeleteMoney(decimal Price)
        {
            var user = _authService.GetUserId();
            var response = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == user);
            //3 
            if (response != null)
            {
                response.Money = response.Money - Price;
                _context.UserMoneys.Update(response);
                await _context.SaveChangesAsync();
                return new ServiceResponse<UserMoney>
                {
                    Data = response,
                    Success = true,
                };
            }
            else
            {
                return new ServiceResponse<UserMoney>
                {
                    Message = "Youre exist dont have chip money account",
                    Success = false,
                };
            }
        }
    }
}
