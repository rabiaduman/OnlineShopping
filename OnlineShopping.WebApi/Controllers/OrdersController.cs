using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Operations.Order;
using OnlineShopping.Business.Operations.Order.Dtos;
using OnlineShopping.WebApi.Jwt;
using OnlineShopping.WebApi.Models;

namespace OnlineShopping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderRequest request)
        {
            var userIdClaim = User.FindFirst(JwtClaimNames.Id)?.Value; //Token'dan UserId bilgisini alıyoruz(string).

            if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
            {
                return Unauthorized("Geçersiz kullanıcı.");
            }

            var addOrderDto = new AddOrderDto
            {
                UserId = userId,
                Products = request.Products.Select(x => new OrderProductDto
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };

            var result = await _orderService.AddOrder(addOrderDto);

            if (!result.IsSucceed)
                return BadRequest(result.Message);
            else
                return Ok(result.Message);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            //_orderService üzerinden Get metodunu çağırıyorum dönen orderı order değişkenine atıyorum
            var order = await _orderService.GetOrderById(id);

            if (order is null)
                return NotFound();
            else
                return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();

            return Ok(orders);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);

            if (!result.IsSucceed)
                return NotFound("Sipariş bulunamadı.");
            else
                return Ok(result.Message);

        }


    }
}
