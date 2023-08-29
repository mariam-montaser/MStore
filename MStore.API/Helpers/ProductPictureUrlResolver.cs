using AutoMapper;
using Microsoft.Extensions.Configuration;
using MStore.API.DTOS;
using MStore.Core.Entities;

namespace MStore.API.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            return null;
        }

      
    }
}
