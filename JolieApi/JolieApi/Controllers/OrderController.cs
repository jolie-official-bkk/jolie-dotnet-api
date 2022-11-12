using System.Net;
using JolieApi.DataContext;
using JolieApi.Models;
using JolieApi.Repository;
using JolieApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JolieApi.Controllers
{
    [ApiController]
    [Route("/api/order/[action]")]
    public class OrderController : ControllerBase
    {
        private IOrderManagerRepository _OrderManager;

        public OrderController(IOrderManagerRepository OrderManager)
        {
            this._OrderManager = OrderManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult getOrders()
        {
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "success",
                data = _OrderManager.GetOrders(),
            });
        }

        //[Authorize]
        [HttpPost]
        public IActionResult placeOrder([FromBody] PlaceOrderRequest order)
        {
            _OrderManager.PlaceOrder(order);
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "success",
            });
        }
    }
}


