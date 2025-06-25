﻿using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DataLayer_POC;
    using DataLayer_POC.Model;

    namespace DotNet_API_POC.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProductController : ControllerBase
        {
            private readonly AppDbContext _context;

            public ProductController(AppDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Product>>> GetAll()
                => await _context.Products.ToListAsync();

            [HttpGet("{id}")]
            public async Task<ActionResult<Product>> GetById(int id)
            {
                var product = await _context.Products.FindAsync(id);
                return product is null ? NotFound() : product;
            }

            [HttpPost]
            public async Task<ActionResult<Product>> Create(Product product)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, Product product)
            {
                if (id != product.Id) return BadRequest();
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product is null) return NotFound();

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }

}
