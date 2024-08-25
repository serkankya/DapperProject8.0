using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Shared.DTOs.OurServices;

namespace Project.UI.Areas.Home.ViewComponents
{
    public class _HomeOurServicesComponentPartial : ViewComponent
    {
        readonly IHttpClientFactory _httpClientFactory;

        public _HomeOurServicesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44340/api/OurService/GetSelectedFourServices");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOurServiceDto>>(jsonData);

                return View(values);
            }

            return View();
        }
    }
}
