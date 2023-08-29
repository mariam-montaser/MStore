using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStore.Core.Entities.OrderAgregate
{
    public class Order: BaseEntity
    {
        public Order()
        {
                
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, decimal subTotal, ICollection<OrderItem> items)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            Items = items;
        }

        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal SubTotal { get; set; }
        public ICollection<OrderItem> Items { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }
    }
}
