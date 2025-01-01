using InveonBootcamp.Business.DTOs.Requests.Course;
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
    public class CoursesController(ICourseService courseService) : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            CreateActionResult(await courseService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            CreateActionResult(await courseService.GetEntityByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateCourseRequest course) =>
            CreateActionResult(await courseService.InsertAsync(course));
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseRequest request) =>
            CreateActionResult(await courseService.UpdateAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            CreateActionResult(await courseService.DeleteAsync(id));

        [HttpGet("GetCoursesByCategory/{category}")]
        public async Task<IActionResult> GetCoursesByCategory(string category) =>
            CreateActionResult(await courseService.GetCoursesByCategoryAsync(category));

        [HttpGet("GetCoursesByName/{courseName}")]
        public async Task<IActionResult> GetCoursesByName(string courseName) =>
            CreateActionResult(await courseService.GetCoursesByNameAsync(courseName));
    }
}
