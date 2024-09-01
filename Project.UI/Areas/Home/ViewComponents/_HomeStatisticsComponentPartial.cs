using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            return View();
        }
    }
}
