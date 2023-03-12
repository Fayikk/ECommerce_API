using Entities;

namespace ECommerce_API.Services.Abstract
{
    public interface IBasketService
    {
        Task<ServiceResponse<Basket>> AddBasket(Basket basket);
        Task<ServiceResponse<Basket>> DeleteBasket(Basket basket);
    }
}
