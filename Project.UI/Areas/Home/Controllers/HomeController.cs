﻿using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Home.Controllers
{
	[Area("Home")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
