using System;
using System.Collections.Generic;
using MStore.Core.Entities.OrderAgregate;

namespace MStore.API.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public string Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }

        public string PaymentIntentId { get; set; }
    }
}
