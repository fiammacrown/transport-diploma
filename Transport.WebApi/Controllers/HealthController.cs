﻿using Microsoft.AspNetCore.Mvc;

namespace Transport.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return Ok();
		}
	}
}
