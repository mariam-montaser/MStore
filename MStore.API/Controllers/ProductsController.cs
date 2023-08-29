using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MStore.API.DTOS;
using MStore.API.Errors;
using MStore.API.Helpers;
using MStore.Core.Entities;
using MStore.Core.IRepository;
using MStore.Core.Specification;

namespace MStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecificationWithBrandAndType(specParams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFilterForCountSpec(specParams);
            var count = await _productRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductSpecificationWithBrandAndType(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound( new ApiErrorResponse(404, "Product Not Found"));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }

    }
}
