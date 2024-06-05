using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;
using System.Linq;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            // Não definir manualmente o valor do Id
            product.Id = 0;

            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            var products = _context.Products
                                   .Where(p => p.Name.Contains(name))
                                   .ToList();
            return Ok(products);
        }
    }
}
