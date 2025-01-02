using AutoMapper;
using Azure.Core;
using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.DTOs.Requests.User;
using InveonBootcamp.Business.DTOs.Responses.User;
using InveonBootcamp.Business.TokenOperations;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;


namespace InveonBootcamp.Business.Concrete
{
    public class MyUserManager(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMailService mailService,
        IConfiguration configuration,
        IMapper mapper) : IUserService
    {
        public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest login)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(c => c.Email == login.Email);
            if (user == null)
            {
                return ServiceResult<LoginResponse>.Fail(
                    new List<string> { "Kullanıcı bulunamadı." },
                    "Email adresi ile eşleşen kullanıcı bulunamadı.",
                    HttpStatusCode.NotFound
                );
            }

            var result = await signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return ServiceResult<LoginResponse>.Fail(
                    new List<string> { "Şifre hatalı." },
                    "Girdiğiniz şifre geçersiz.",
                    HttpStatusCode.BadRequest
                );
            }

            TokenHandler tokenHandler = new(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate;
            await userManager.UpdateAsync(user);

            return ServiceResult<LoginResponse>.Success(
                new LoginResponse { Message = "Giriş Başarılı", AccessToken = token.AccessToken },
                "Başarıyla giriş yapıldı.",
                HttpStatusCode.OK
            );
        }


        public async Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest register)
        {
            if (register.Password == null || register.UserName == null || register.Email == null)
            {
                return ServiceResult<RegisterResponse>.Fail(
                    new List<string> { "Alanlar boş bırakılamaz." },
                    "Kullanıcı oluşturulurken bazı alanlar eksik.",
                    HttpStatusCode.BadRequest
                );
            }

            var user = new User { Email = register.Email, UserName = register.UserName };

            TokenHandler tokenHandler = new(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate;

            var result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return ServiceResult<RegisterResponse>.Fail(
                    new List<string> { "Kullanıcı oluşturulamadı." },
                    "Kullanıcı oluşturulurken bir hata oluştu.",
                    HttpStatusCode.BadRequest
                );
            }

            var emailSubject = "Kayıt Başarılı";
            var emailBody = "<strong>Hesabınız başarıyla oluşturulmuştur.</strong><br/>" +
                            "Hesabınızı kullanarak giriş yapabilirsiniz.";

            // E-posta gönder
            await mailService.SendMessageAsync(register.Email, emailSubject, emailBody, isBodyHtml: true);



            return ServiceResult<RegisterResponse>.Success(
                new RegisterResponse { Message = "Kayıt Başarılı" },
                "Kullanıcı başarıyla kaydedildi.",
                HttpStatusCode.Created
            );
        }


        public async Task<ServiceResult> UpdateUserAsync(UpdateUserRequest user)
        {
            if (user == null)
            {
                return ServiceResult.Fail(
                    "Güncellenecek kullanıcı verisi geçersiz.",
                    HttpStatusCode.BadRequest
                );
            }

            var existingUser = await userManager.FindByIdAsync(user.Id.ToString());
            if (existingUser == null)
            {
                return ServiceResult.Fail(
                    "Kullanıcı bulunamadı.",
                    HttpStatusCode.NotFound
                );
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;

            await userManager.UpdateAsync(existingUser);

            return ServiceResult.Success("Kullanıcı başarıyla güncellendi.", HttpStatusCode.OK);
        }


        public async Task<ServiceResult> LogoutAsync(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return ServiceResult.Fail(
                    "Kullanıcı bulunamadı.",
                    HttpStatusCode.NotFound
                );
            }

            await signInManager.SignOutAsync(); // result değişkenine gerek yok
            return ServiceResult.Success("Başarıyla çıkış yapıldı.", HttpStatusCode.OK);
        }


        public async Task<ServiceResult<GetUserByIdResponse>> GetUserByIdAsync(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            var newUser = mapper.Map<GetUserByIdResponse>(user);
            if (user == null)
            {
                return ServiceResult<GetUserByIdResponse>.Fail(new List<string> { "Kullanıcı bulunamadı." }, "Kullanıcı bulunamadı.", HttpStatusCode.NotFound);
            }

            return ServiceResult<GetUserByIdResponse>.Success(newUser, "Kullanıcı başarıyla bulundu.", HttpStatusCode.OK);
        }


        // Kullanıcı Silme - Kullanıcıyı silme işlemi
        public async Task<ServiceResult> DeleteUserAsync(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return ServiceResult.Fail("Kullanıcı bulunamadı.", HttpStatusCode.NotFound);
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return ServiceResult.Fail("Kullanıcı silinemedi.", HttpStatusCode.BadRequest);
            }

            return ServiceResult.Success("Kullanıcı başarıyla silindi.", HttpStatusCode.OK);
        }


        //public async Task<ServiceResult> ForgotPasswordAsync(string email)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return ServiceResult.Fail("Kullanıcı bulunamadı.", HttpStatusCode.NotFound);
        //    }

        //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
        //    var resetLink = $"https://example.com/reset-password?token={token}"; // Burada şifre sıfırlama linkini oluşturuyorsunuz.

        //    // E-posta gönderme işlemi yapılacak burada
        //    // Örneğin, bir e-posta gönderici servisi kullanarak `resetLink` gönderilebilir

        //    return ServiceResult.Success("Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.", HttpStatusCode.OK);
        //}
    }
}
