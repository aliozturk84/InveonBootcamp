using Microsoft.AspNetCore.Identity;

namespace LibraryManagementApp.Web.Models.Repositories
{
    //Not: Henüz bu sayfadaki ekstra Usertype userfeature ve enum kullanılmadı, ilerde geliştirince kullanırım diye silmedim.
    public enum UserType : byte
    {
        Normal = 1,
        Foreigner = 2,
        Vip = 3
    }

    public class AppUser : IdentityUser<Guid>
    {
        public UserType UserType { get; set; }

        public UserFeature? UserFeature { get; set; }
    }
}
