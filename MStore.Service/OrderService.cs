using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities;
using MStore.Core.Entities.OrderAgregate;
using MStore.Core.IRepository;
using MStore.Core.IService;
using MStore.Core.Specification;

namespace MStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<Order> _orderRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;

        public OrderService(
            IBasketRepository basketRepo,
            IUnitOfWork unitOfWork
            //IGenericRepository<Product> productRepo,
            //IGenericRepository<Order> orderRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethodRepo
            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            //_productRepo = productRepo;
            //_orderRepo = orderRepo;
            //_deliveryMethodRepo = deliveryMethodRepo;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(BasketId);
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productToOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productToOrder, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            var order = new Order(BuyerEmail, ShippingAddress, deliveryMethod, subTotal, orderItems);
            await _unitOfWork.Repository<Order>().CreateAsync(order);
            var result = await _unitOfWork.Complete();
            if(result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithOrderItemForUserSpec(buyerEmail);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }

        public async Task<Order> GetOrderByIdOrderAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderWithOrderItemForUserSpec(buyerEmail, orderId);
            return await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

        }
    }
}
