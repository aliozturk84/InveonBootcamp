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
    public class PaymentsController(IPaymentService paymentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await paymentService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await paymentService.GetEntityByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Payment payment)
        {
            await paymentService.InsertAsync(payment);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Payment payment)
        {
            await paymentService.UpdateAsync(payment);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await paymentService.GetEntityByIdAsync(id);
            await paymentService.DeleteAsync(payment);
            return Ok();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] int id)
        {
            var payment = await paymentService.GetEntityAsync(c => c.Id == id);
            return Ok(payment);
        }
    }
}
