using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Shared.DTOs.TestimonialDtos;

namespace Project.UI.Areas.Home.ViewComponents
{
	public class _HomeTestimonialsComponentPartial : ViewComponent
	{
		readonly IHttpClientFactory _httpClientFactory;

		public _HomeTestimonialsComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:44340/api/Testimonial/GetActiveTestimonials");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
