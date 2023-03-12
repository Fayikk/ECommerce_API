
using Entities;

namespace ECommerce_API.Services.Abstract
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetOrderByUser();
        Task<ServiceResponse<List<Order>>> StoreCartItem(List<Order> order);
        Task<ServiceResponse<bool>> Refund(int orderId);
    }
}
