using Application.Dto;
using Application.Dtos.Request;
using AutoMapper;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/products
        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        /// <response code="200">Lista de produtos retornada com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // GET: api/products/{id}
        /// <summary>
        /// Retorna um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <response code="200">Produto encontrado com sucesso.</response>
        /// <response code="404">Produto não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Produto não encontrado.");

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        // POST: api/products
        /// <summary>
        /// Adiciona um novo produto.
        /// </summary>
        /// <param name="productRequest">Detalhes do produto a ser criado.</param>
        /// <response code="201">Produto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductRequest productRequest)
        {
            var productEntity = _mapper.Map<Product>(productRequest);
            var createdProduct = await _productService.CreateProductAsync(productEntity);
            var createdProductDto = _mapper.Map<ProductDto>(createdProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.ProductID }, createdProductDto);
        }

        // PUT: api/products/{id}
        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="productRequest">Dados atualizados do produto.</param>
        /// <response code="200">Produto atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Produto não encontrado.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
        {
            var productEntity = _mapper.Map<Product>(productRequest);
            productEntity.ProductId = id; // Atualiza o ID na entidade mapeada

            var updated = await _productService.UpdateProductAsync(productEntity);
            if (!updated)
                return NotFound("Produto não encontrado.");

            return Ok("Produto atualizado com sucesso.");
        }

        // DELETE: api/products/{id}
        /// <summary>
        /// Remove um produto.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <response code="200">Produto removido com sucesso.</response>
        /// <response code="404">Produto não encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted)
                return NotFound("Produto não encontrado.");

            return Ok("Produto removido com sucesso.");
        }
    }
}
