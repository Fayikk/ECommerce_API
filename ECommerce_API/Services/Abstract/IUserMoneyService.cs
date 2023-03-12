using Entities;

namespace ECommerce_API.Services.Abstract
{
    public interface IUserMoneyService
    {
        Task<ServiceResponse<UserMoney>> AddMoney(UserMoney userMoney);
        Task<ServiceResponse<UserMoney>> DeleteMoney(decimal Price);
    }
}
