using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using DataLayer_POC.Model;

namespace Web_Layer.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var client = new HttpClient();
            var product = new Product { Name = "Laptop", Price = 75000 };

            // Create
            var json = JsonSerializer.Serialize(product);
            var response = await client.PostAsync("https://localhost:xxxx/api/product",
                new StringContent(json, Encoding.UTF8, "application/json"));

            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return View();
        }
        
    }
}
