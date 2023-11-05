using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Customer : AppUser
    {
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public Customer()
        {
            ActivationCode = Guid.NewGuid();
            Role = UserRole.Customer;
            MonthlyDate = DateTime.Now;
        }
        public Guid ActivationCode { get; set; } //Kullanıcıya üyelik onayı icin gönderilen unique bir sifreleme tipi (Guid)
        public DateTime MonthlyDate { get; set; }
        public bool Active { get; set; }
        public string VipID { get; set; }
        public bool VipPayed { get; set; }
        public bool Monthly { get; set; }
        public int FriendPayed { get; set; }
        public string Job { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public decimal? Currency { get; set; }
        public CustomerRole CustomerCategory { get; set; }
        //Relational Property
        public virtual List<Order> Orders { get; set; }
        public virtual List<Reservation> Rezervasyons { get; set; }
    }
}
