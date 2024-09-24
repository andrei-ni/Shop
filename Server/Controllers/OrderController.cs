using DrPrint.Server.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DrPrint.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder([FromBody] OrderDTO request)
        {
            var result = await _orderService.PlaceOrder(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet("admin/orders")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("{orderId}")]
		public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> GetOrdersDetails(int orderId)
		{
			var result = await _orderService.GetOrderDetails(orderId);
			return Ok(result);
		}

		[HttpGet("admin")]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> AdminGetOrders()
        {
            var result = await _orderService.AdminGetOrders();
            return Ok(result);
        }

		[HttpGet("admin/{orderId}")]
		public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> AdminGetOrdersDetails(int orderId)
		{
			var result = await _orderService.AdminGetOrderDetails(orderId);
			return Ok(result);
		}

    }
}
