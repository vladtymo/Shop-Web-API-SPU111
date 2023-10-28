using BusinessLogic.ApiModels.Products;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop_api_spu111.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        //[HttpGet]                     // GET ~/api/products
        [HttpGet("all")]                // GET ~/api/products/all
        //[HttpGet("/all-products")]    // GET ~/all-products
        public IActionResult Get()
        {
            return Ok(productsService.Get()); // status: 200
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdFromRoute([FromRoute] int id) // [FromQuery] [FromFrom]...
        {
            return Ok(productsService.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            productsService.Create(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit([FromBody] EditProductModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            productsService.Edit(model);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            productsService.Delete(id);
            return Ok();
        }
    }
}
