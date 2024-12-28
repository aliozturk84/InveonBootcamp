using InveonBootcamp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        // Foreign Key: Siparişin sahibi kullanıcı
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign Key: Siparişin içeriğindeki kurs
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Navigation Property: Siparişin ödemesi
        public Payment Payment { get; set; }
    }
}
