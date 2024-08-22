using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Shared.DTOs.HomeContentDtos;

namespace Project.UI.Areas.Home.ViewComponents
{
    public class _HomeContentComponentPartial : ViewComponent
    {
        readonly IHttpClientFactory _httpClientFactory;

        public _HomeContentComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44340/api/HomeContent/GetHomeContent");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultHomeContentDto>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
