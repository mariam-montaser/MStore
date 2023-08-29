using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MStore.Core.Entities;
using MStore.Core.Entities.OrderAgregate;

namespace MStore.Repository.Data
{
    public class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../MStore.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                }
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../MStore.Repository/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                    {
                        context.Set<ProductType>().Add(type);
                    }
                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../MStore.Repository/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliveryData = File.ReadAllText("../MStore.Repository/Data/DataSeed/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                    foreach (var method in methods)
                    {
                        context.Set<DeliveryMethod>().Add(method);
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError("Seeding Error: ", ex.Message);
            }
        }
    }
}
