using Microsoft.AspNetCore.Mvc;
using NZWalls.UI.Models.DTO;

namespace NZWalls.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                var client=httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7089/api/regions");
                 
                httpResponseMessage.EnsureSuccessStatusCode();

               response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                
               
            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }

            return View(response);
        }
    }
}
