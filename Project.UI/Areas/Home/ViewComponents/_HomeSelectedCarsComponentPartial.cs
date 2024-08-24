using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Shared.DTOs.VehicleDtos;

namespace Project.UI.Areas.Home.ViewComponents
{
    public class _HomeSelectedCarsComponentPartial : ViewComponent
    {
        readonly IHttpClientFactory _httpClientFactory;

        public _HomeSelectedCarsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44340/api/Vehicle/GetSelectedCars");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultVehicleDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
