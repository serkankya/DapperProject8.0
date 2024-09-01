using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.MessageDtos;
using Project.UI.Models;
using System.Net.Http;
using System.Text;

namespace Project.UI.Areas.Contact.ViewComponents
{
    public class _ContactSendMessageComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
