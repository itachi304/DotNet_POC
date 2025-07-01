using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DataLayer_POC;
    using DataLayer_POC.Model;
    using Shared_Layer;

    namespace DotNet_API_POC.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProductController : ControllerBase
        {
            private readonly AppDbContext _context;

            public ProductController(AppDbContext context) => _context = context;
            [HttpGet]
            public async Task<ActionResult<ApiResponse<List<Product>>>> GetAll()
            {
                var data = await _context.Products.ToListAsync();
                return Ok(ApiResponse<List<Product>>.SuccessResponse(data));
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ApiResponse<Product>>> Get(int id)
            {
                var order = await _context.Products.FindAsync(id);
                return order is null
                    ? NotFound(ApiResponse<Product>.ErrorResponse("Order not found"))
                    : Ok(ApiResponse<Product>.SuccessResponse(order));
            }

            [HttpPost]
            public async Task<ActionResult<ApiResponse<Product>>> Create(Product order)
            {
                _context.Products.Add(order);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = order.Id }, ApiResponse<Product>.SuccessResponse(order));
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<ApiResponse<Product>>> Update(int id, Product order)
            {
                if (id != order.Id) return BadRequest(ApiResponse<Product>.ErrorResponse("ID mismatch"));
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(ApiResponse<Product>.SuccessResponse(order, "Updated successfully"));
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
            {
                var order = await _context.Products.FindAsync(id);
                if (order is null) return NotFound(ApiResponse<string>.ErrorResponse("Order not found"));
                _context.Products.Remove(order);
                await _context.SaveChangesAsync();
                return Ok(ApiResponse<string>.SuccessResponse("Deleted successfully"));
            }
        }
    }

}
