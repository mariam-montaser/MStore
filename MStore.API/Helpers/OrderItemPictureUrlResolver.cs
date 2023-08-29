using AutoMapper;
using Microsoft.Extensions.Configuration;
using MStore.API.DTOS;
using MStore.Core.Entities.OrderAgregate;

namespace MStore.API.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.Product.PictureUrl}";
            return null;
        }
    }
}
