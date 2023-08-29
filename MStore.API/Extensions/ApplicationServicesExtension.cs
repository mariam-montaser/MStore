using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MStore.API.Errors;
using MStore.API.Helpers;
using MStore.Core.IRepository;
using MStore.Core.IService;
using MStore.Repository;
using MStore.Service;

namespace MStore.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(m => m.Value.Errors.Count() > 0)
                                                         .SelectMany(m => m.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToArray();
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };


                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
