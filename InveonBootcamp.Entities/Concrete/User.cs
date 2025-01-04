using InveonBootcamp.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Entities.Concrete
{
    public class User : IdentityUser<int>, IEntity
    {
        public ICollection<Order> Orders { get; set; } // Navigation Property
        //public ICollection<Course> Courses { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
