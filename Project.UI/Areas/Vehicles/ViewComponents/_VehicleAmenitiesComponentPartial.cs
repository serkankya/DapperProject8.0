using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.VehicleAmenitiesDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Vehicles.ViewComponents
{
	public class _VehicleAmenitiesComponentPartial : ViewComponent
	{
		readonly IHttpClientFactory _httpClientFactory;
		readonly ApiSettings _apiSettings;

		public _VehicleAmenitiesComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_apiSettings = apiSettings.Value;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			id = ViewBag.VehicleId;

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var responseMessage = await client.GetAsync("Vehicle/GetVehicleAmenities/" + id);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultVehicleAmenitiesDto>>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
