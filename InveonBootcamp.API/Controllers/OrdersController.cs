using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.DTOs.Requests.Order;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            CreateActionResult(await orderService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            CreateActionResult(await orderService.GetEntityByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateOrderRequest order) =>
            CreateActionResult(await orderService.InsertAsync(order));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderRequest request) =>
            CreateActionResult(await orderService.UpdateAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            CreateActionResult(await orderService.DeleteAsync(id));

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] int id) =>
            CreateActionResult(await orderService.GetEntityAsync(c => c.Id == id));

        [HttpGet("GetOrdersByUserId/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId) =>
            CreateActionResult(await orderService.GetOrdersByUserIdAsync(userId));
    }
}
