using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities;

namespace MStore.Core.Specification
{
    public class ProductWithFilterForCountSpec: BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpec(ProductSpecParams productParams)
            :base( p => 
                            (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId) &&
                            (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId) &&
                            (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search))
            )
        {

        }
    }
}
