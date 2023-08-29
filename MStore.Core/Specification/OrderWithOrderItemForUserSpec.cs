using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities.OrderAgregate;

namespace MStore.Core.Specification
{
    public class OrderWithOrderItemForUserSpec: BaseSpecification<Order>
    {
        public OrderWithOrderItemForUserSpec(string buyerEmail): base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderWithOrderItemForUserSpec(string buyerEmail, int orderId) : base(O => O.BuyerEmail == buyerEmail &&  O.Id == orderId)
        {
            Includes.Add(O => O.DeliveryMethod);

            Includes.Add(O => O.Items);

        }
    }
}
