using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IPokemonRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IPokemonRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_repository.GetAllOrders()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Order>> Get(int id)
        {
            try
            {
                var product = _repository.GetOrderById(id);
                if (product != null)
                {
                    return Ok(_mapper.Map<Order, OrderViewModel>(product));
                }
                else
                {
                    return NotFound($"Order not found: id: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GET orders: {ex}");
                return BadRequest($"Failed to GET orders: {id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    //var newOrder = new Order()
                    //{
                    //	Id = model.OrderId,
                    //	OrderDate = model.OrderDate
                    //};

                    // Validation
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        // Create VM so that communication between M and VM is two-way
                        //var viewModel = new OrderViewModel()
                        //{
                        //	OrderId = newOrder.Id,
                        //	OrderDate = newOrder.OrderDate
                        //};

                        return Created($"api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to POST order: {ex}");
            }

            return BadRequest($"Failed to POST new order");
        }
    }
}
