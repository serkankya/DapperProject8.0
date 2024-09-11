using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.MessageDtos;
using Project.UI.Models;
using Project.UI.Tools.FluentValidation;
using System.Net.Http;
using System.Text;

namespace Project.UI.Areas.Contact.Controllers
{
	[Area("Contact")]
	public class ContactController : Controller
	{
		readonly IHttpClientFactory _httpClientFactory;
		readonly ApiSettings _apiSettings;

		public ContactController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_apiSettings = apiSettings.Value;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(InsertMessageDto insertMessageDto)
		{
			var validator = new InsertMessageValidator();
			var result = validator.Validate(insertMessageDto);

			if (!result.IsValid)
			{
				TempData["ErrorMessage"] = result.Errors.Select(e => e.ErrorMessage).ToList();
				return RedirectToAction("Index");
			}

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var jsonData = JsonConvert.SerializeObject(insertMessageDto);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Message/InsertMessage", content);

			if (responseMessage.IsSuccessStatusCode)
			{
				TempData["SuccessMessage"] = "Mesaj başarıyla gönderildi.";
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}
