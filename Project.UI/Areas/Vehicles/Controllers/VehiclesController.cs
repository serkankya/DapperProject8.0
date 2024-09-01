using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.VehicleDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Vehicles.Controllers
{
	[Area("Vehicles")]
	public class VehiclesController : Controller
	{
		readonly IHttpClientFactory _httpClientFactory;
		readonly ApiSettings _apiSettings;

		public VehiclesController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_apiSettings = apiSettings.Value;
		}

		public IActionResult Index()
		{
			return View();
		}

		//Car details
		[HttpGet("Details/{id}")]
		public async Task<IActionResult> Details(int id)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var responseMessage = await client.GetAsync("Vehicle/GetVehicle/" + id);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultVehicleDto>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
