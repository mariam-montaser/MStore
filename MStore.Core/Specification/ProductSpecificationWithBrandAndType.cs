using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities;

namespace MStore.Core.Specification
{
    public class ProductSpecificationWithBrandAndType: BaseSpecification<Product>
    {
        public ProductSpecificationWithBrandAndType(ProductSpecParams productParams)
            :base(p =>
                        (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId) &&
                        (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId) &&
                        (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search))

            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            int skip = productParams.PageSize * (productParams.PageIndex - 1);

            ApplyPagination(skip, productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

        }

        public ProductSpecificationWithBrandAndType(int id): base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
