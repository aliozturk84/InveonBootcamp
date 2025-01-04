using InveonBootcamp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InveonBootcamp.Entities.Concrete
{
    public class Course : IEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        //public int InstructorId { get; set; }
        //public User Instructor{ get; set; }

        // Navigation Property: Kursun siparişleri
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}