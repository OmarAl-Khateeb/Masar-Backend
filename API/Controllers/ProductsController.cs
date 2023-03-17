using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructue.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class ProductsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<StoreContext> _unitOfWork;
        private readonly IUploadService _uploadService;

        public ProductsController(IUnitOfWork<StoreContext> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _uploadService = uploadService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _unitOfWork.Repository<Product, StoreContext>().CountAsync(countSpec);
            var products = await _unitOfWork.Repository<Product, StoreContext>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _unitOfWork.Repository<Product, StoreContext>().GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductDto>(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductCDto productCDto)
        {
            var product = _mapper.Map<ProductCDto, Product>(productCDto);
            product.GymId = User.GetGymId();

            _unitOfWork.Repository<Product, StoreContext>().Add(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Product"));

            return Created("test", product);
        }

        [HttpPost("Upload/{id}")]
        public async Task<ActionResult> Upload(IFormFile file, int id)
        {
            var product = await _unitOfWork.Repository<Product, StoreContext>().GetByIdAsync(id);

            if (product == null) return NotFound(new ApiResponse(404));

            var uploadFile = await _uploadService.UploadAsync(file, "images/products");

            product.PictureUrl = "images/products/" + uploadFile.FileName;

            _unitOfWork.Repository<Product, StoreContext>().Update(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Product"));
            
            return Ok(new { uploadFile.FileName });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductCDto productCDto)
        {
            var product = await _unitOfWork.Repository<Product, StoreContext>().GetByIdAsync(id);

            if (product == null) return NotFound(new ApiResponse(404));

            _mapper.Map<ProductCDto, Product>(productCDto, product);

            _unitOfWork.Repository<Product, StoreContext>().Update(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Product"));

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Repository<Product, StoreContext>().GetByIdAsync(id);

            if (product == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Product, StoreContext>().Delete(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Product"));

            return NoContent();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _unitOfWork.Repository<ProductBrand, StoreContext>().ListAllAsync());
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductCategories()
        {
            return Ok(await _unitOfWork.Repository<ProductCategory, StoreContext>().ListAllAsync());
        }
    }
}