using InveonBootcamp.Business.DTOs.Requests.Payment;
using InveonBootcamp.Business.DTOs.Requests.User;
using InveonBootcamp.Business.DTOs.Responses.User;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Abstract
{
    public interface IUserService
    {
        Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest login);
        Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest register);
        Task<ServiceResult> UpdateUserAsync(UpdateUserRequest user);
        Task<ServiceResult> LogoutAsync(int userId);
        Task<ServiceResult> DeleteUserAsync(int userId);
        Task<ServiceResult<GetUserByIdResponse>> GetUserByIdAsync(int userId);  
    }
}
