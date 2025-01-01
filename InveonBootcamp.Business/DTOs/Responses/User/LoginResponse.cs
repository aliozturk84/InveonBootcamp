using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace InveonBootcamp.Business.DTOs.Responses.User
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string AccessToken { get; set; }
    }
}
