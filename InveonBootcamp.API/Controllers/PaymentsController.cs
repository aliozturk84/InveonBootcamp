using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.DTOs.Requests.Payment;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentService paymentService) : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            CreateActionResult(await paymentService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            CreateActionResult(await paymentService.GetEntityByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreatePaymentRequest payment) =>
            CreateActionResult(await paymentService.InsertAsync(payment));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentRequest request) =>
            CreateActionResult(await paymentService.UpdateAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            CreateActionResult(await paymentService.DeleteAsync(id));

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] int id) =>
            CreateActionResult(await paymentService.GetEntityAsync(c => c.Id == id));

        [AllowAnonymous]
        [HttpPost("PayCallBack")]
        public async Task<IActionResult> PayCallBack([FromForm] IFormCollection requestBody) =>
            CreateActionResult(await paymentService.PayCallBack(requestBody));

    }
}
