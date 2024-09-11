using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.UI.Models;

namespace Project.UI.Areas.Home.ViewComponents
{
    public class _HomeStatisticsComponentPartial : ViewComponent
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly ApiSettings _apiSettings;

        public _HomeStatisticsComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);

            var vehicleCountResponse = await client.GetAsync("Statistics/GetVehicleCount");
            var vehicleJsonData = await vehicleCountResponse.Content.ReadAsStringAsync();
            ViewBag.VehicleCount = vehicleJsonData;

            var blogCountResponse = await client.GetAsync("Statistics/GetBlogCount");
            var blogJsonData = await blogCountResponse.Content.ReadAsStringAsync();
            ViewBag.BlogCount = blogJsonData;

            var reviewCountResponse = await client.GetAsync("Statistics/GetReviewCount");
            var reviewJsonData = await reviewCountResponse.Content.ReadAsStringAsync();
            ViewBag.ReviewCount = reviewJsonData;

            return View();
        }
    }
}
