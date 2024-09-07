using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.BlogDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Blog.ViewComponents
{
    public class _SingleBlogRecentBlogsComponentPartial : ViewComponent
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly ApiSettings _apiSettings;

        public _SingleBlogRecentBlogsComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
            var responseMessage = await client.GetAsync("Blog/GetRecentBlogs");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
