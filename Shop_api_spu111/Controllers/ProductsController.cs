using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop_api_spu111.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopSPUDbContext ctx;

        public ProductsController(ShopSPUDbContext ctx)
        {
            this.ctx = ctx;
        }

        //[HttpGet]                     // GET ~/api/products
        [HttpGet("all")]                // GET ~/api/products/all
        //[HttpGet("/all-products")]    // GET ~/all-products
        public IActionResult Get()
        {
            return Ok(ctx.Products.ToList()); // status: 200
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdFromRoute([FromRoute] int id) // [FromQuery] [FromFrom]...
        {
            Console.WriteLine("Route....");
            var item = ctx.Products.Find(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Products.Add(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Products.Update(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) return NotFound();

            ctx.Products.Remove(item);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
