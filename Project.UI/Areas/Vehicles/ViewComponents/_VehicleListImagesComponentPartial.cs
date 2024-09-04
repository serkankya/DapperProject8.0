using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.VehicleDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Vehicles.ViewComponents
{
    public class _VehicleListImagesComponentPartial : ViewComponent
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly ApiSettings _apiSettings;

        public _VehicleListImagesComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            id = ViewBag.VehicleId;

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
            var responseMessage = await client.GetAsync("Vehicle/GetVehicleImages/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultVehicleImagesDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
