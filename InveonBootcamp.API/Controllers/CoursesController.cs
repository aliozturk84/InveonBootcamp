using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(ICourseService courseService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await courseService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await courseService.GetEntityByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Course course)
        {
            await courseService.InsertAsync(course);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course course)
        {
            await courseService.UpdateAsync(course);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await courseService.GetEntityByIdAsync(id);
            await courseService.DeleteAsync(course);
            return Ok();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string name)
        {
            var course = await courseService.GetEntityAsync(c => c.Name == name);
            return Ok(course);
        }
    }
}
