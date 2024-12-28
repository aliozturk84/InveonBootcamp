using Azure.Core;
using InveonBootcamp.API.DTOs.Requests.User;
using InveonBootcamp.API.DTOs.Responses.User;
using InveonBootcamp.API.TokenOperations;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InveonBootcamp.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] User user)
        {
            await userManager.CreateAsync(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(c => c.Email == login.Email );
            if(user == null)
            {
                throw new Exception("Not implemented");
            }

            var result = await signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest("şifre hatalı (düzenle)");
            }
        
            TokenHandler tokenHandler = new(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate;
            await userManager.UpdateAsync(user);

            return Ok(new LoginResponse { Message = "Giriş Başarılı", AccessToken = token.AccessToken });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            if(register.Password == null || register.UserName == null || register.Email == null)
            {
                return BadRequest("Alanlar boş bırakılamaz");
            }
                
            var user = new User { Email = register.Email, UserName = register.UserName };

            TokenHandler tokenHandler = new(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate;

            var result = await userManager.CreateAsync(user, register.Password);

            return Ok(new RegisterResponse { Message = result.ToString() });
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] User user)
        {
            await userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string email)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(c => c.Email == email);
            return Ok(user);
        }


    }
}
