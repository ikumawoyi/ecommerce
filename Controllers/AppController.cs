using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DutchTreat.Controllers
{
	public class AppController : Controller
	{
		private readonly IMailService _mailService;
		private readonly IDutchRepository _repository;


		public AppController(IMailService mailService, IDutchRepository repository)
		{
			_mailService = mailService;
			_repository = repository;
		}
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			return View();
		}

		[HttpPost("contact")]
		public IActionResult Contact(ContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				_mailService.SendMessage("platinny10@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
				ViewBag.UserMessage = "Mail Sent";
				ModelState.Clear();
			}
			return View();
		}

		public IActionResult About()
		{
			ViewBag.Title = "About Us";
			return View();
		}

		public IActionResult Shop()
		{
			return View();
		}
	}
}
