using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.API.Controllers
{
    //[Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await orderService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await orderService.GetEntityByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Order order)
        {
            await orderService.InsertAsync(order);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            await orderService.UpdateAsync(order);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await orderService.GetEntityByIdAsync(id);
            await orderService.DeleteAsync(order);
            return Ok();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] int id)
        {
            var course = await orderService.GetEntityAsync(c => c.Id == id);
            return Ok(course);
        }
    }
}
