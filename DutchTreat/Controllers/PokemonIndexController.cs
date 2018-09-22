using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DutchTreat.Controllers
{
	public class PokemonIndexController : Controller
	{
		private readonly IMessageService messageService;
		private readonly IPokemonRepository _dbRepository;

        public PokemonIndexController(IMessageService messageService, IPokemonRepository dbRepository)
		{
			this.messageService = messageService;
			_dbRepository = dbRepository;
        }
		public IActionResult Index()
		{
			ViewBag.Title = "This is the pokemon index";
			return View();
		}

		[HttpGet("pikachu")]
		public IActionResult Pikachu()
		{
			ViewBag.Title = "Pikachu";
			return View();
		}

		[HttpGet("bat")]
		public IActionResult Bat()
		{
			ViewBag.Title = "Bat";
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
				this.messageService.SendMessage(model.Message);
				ViewBag.UserMessage = "Message sent";
				// clear the message
				ModelState.Clear();
			}

			return View();
		}

		[Authorize]
		public IActionResult Shop()
		{
			var products = _dbRepository.GetAllProducts().ToList();

			return View(products);
		}
	}
}
