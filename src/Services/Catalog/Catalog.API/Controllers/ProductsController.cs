using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/catalog/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsRepository _productsRepository;

        public ProductsController(ILogger<ProductsController> logger, IProductsRepository repository)
        {
            _logger = logger;
            _productsRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productsRepository.GetProducts());
        }

        [HttpGet("{id:length(24)}", Name = nameof(GetProduct))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(string id) 
        {
            var product = await _productsRepository.GetProduct(id);

            if (product != null)
                return Ok(product);

            _logger.LogError($"Product with id: {id} was not found");
            return NotFound();
        }

        [HttpGet("[action]/{category}",Name = nameof(GetProductsByCategory))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var filteredProducts = await _productsRepository.GetProductsByCategory(category);
            return Ok(filteredProducts);
        }

        [HttpGet("[action]/{name}", Name = nameof(GetProductsByName))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            var filteredProducts = await _productsRepository.GetProductsByName(name);
            return Ok(filteredProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product) 
        {
            await _productsRepository.CreateProduct(product);

            return CreatedAtRoute(routeName: nameof(GetProduct), routeValues: new { id = product.Id }, product);
        }

        [HttpPut(Name = nameof(UpdateProduct))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            await _productsRepository.UpdateProduct(product);

            return Ok(product);
        }

        [HttpDelete("{id:Length(24)}", Name = nameof(DeleteProduct))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {            
            return Ok(await _productsRepository.DeleteProduct(id));
        }
    }
}
