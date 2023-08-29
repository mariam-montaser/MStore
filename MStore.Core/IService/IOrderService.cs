using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities.OrderAgregate;

namespace MStore.Core.IService
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        Task<Order> GetOrderByIdOrderAsync(string BuyerEmail, int OrderId);

        Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    }
}
