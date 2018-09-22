﻿using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ProductsController : Controller
	{
		private readonly IPokemonRepository _repository;
		private readonly ILogger<ProductsController> _logger;

		public ProductsController(IPokemonRepository repository, ILogger<ProductsController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public ActionResult<IEnumerable<Product>> Get()
		{
			try
			{
				return Ok(_repository.GetAllProducts());

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get products: {ex}");
				return BadRequest("Failed to get products.");
			}
		}
	}
}