using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                var client = httpClientFactory.CreateClient();
                var httpresponseMessage = await client.GetAsync("https://localhost:7089/api/regions");
                httpresponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpresponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
   
            }
            catch (Exception ex)
            {
                //log exception
               
            }

            return View(response);
        }
        [HttpGet]

        public IActionResult Add()
        {

        return View(); 
        
        
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            var client= httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7089/api/regions"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(addRegionViewModel), System.Text.Encoding.UTF8, "application/json"),

            };
             var response=await client.SendAsync(httpRequestMessage);
              response.EnsureSuccessStatusCode();
            var res=await response.Content.ReadFromJsonAsync<RegionDto>();

            if(res is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();


        }

        [HttpGet]

        public async Task<IActionResult> Edit (Guid id)
        {
            var client=httpClientFactory.CreateClient();

            var httpRes = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7089/api/regions/{id.ToString()}");
            if (httpRes is not null) { 
                return View(httpRes);
            }
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpreq = new HttpRequestMessage()
                {

                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7089/api/regions/{request.Id}"),
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json"),
                };
                var httpres = await client.SendAsync(httpreq);
                httpres.EnsureSuccessStatusCode();


                var res = await httpres.Content.ReadFromJsonAsync<RegionDto>();

                if (res is not null)
                {
                    return RedirectToAction("Edit", "Regions");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Something went wrong");
            }
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient();
                var httpres = await httpClient.DeleteAsync($"https://localhost:7089/api/regions/{request.Id}");
                httpres.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                //console
                
            }
            return View("Edit");
        }
    }

}
