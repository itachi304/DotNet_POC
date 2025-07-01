using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using DataLayer_POC.Model;
using Shared_Layer;

namespace Web_Layer.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;

        public ProductController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("MyApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("order");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<Product>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(result?.Data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product order)
        {
            var json = JsonSerializer.Serialize(order);
            var response = await _client.PostAsync("order", new StringContent(json, Encoding.UTF8, "application/json"));
            return RedirectToAction("Index");
        }

    }
}
