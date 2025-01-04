using Azure.Core;
using InveonBootcamp.Business.DTOs.Requests.User;
using InveonBootcamp.Business.DTOs.Responses.User;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InveonBootcamp.Business.Concrete;
using InveonBootcamp.Business.Abstract;

namespace InveonBootcamp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IMailService mailService) : CustomControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login) =>
            CreateActionResult(await userService.LoginAsync(login));

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register) =>
            CreateActionResult(await userService.RegisterAsync(register));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateUserRequest request) =>
            CreateActionResult(await userService.UpdateUserAsync(request));

        [AllowAnonymous]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId) =>
            CreateActionResult(await userService.GetUserByIdAsync(userId));

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(int userId) =>
            CreateActionResult(await userService.LogoutAsync(userId));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
        CreateActionResult(await userService.DeleteUserAsync(id));

        [Authorize]
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateCurrentUserRequest request)
        {
            // Kullanıcı profilini güncelle
            var result = await userService.UpdateCurrentUserAsync(request, User);

            // Başarılı sonuç dönerse 200 OK
            if (result.IsSuccess)
            {
                return Ok(new
                {
                    result.Message
                    
                });
            }

            // Başarısız sonuç dönerse hata mesajlarını ve HTTP durum kodunu gönder
            return StatusCode((int)result.Status, new
            {
                result.Message,
                Errors = result.ErrorMessage
            });
        }

    }
}
