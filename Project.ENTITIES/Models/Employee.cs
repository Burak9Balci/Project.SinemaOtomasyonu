using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Employee : AppUser
    {
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public string Department { get; set; }
        //Relational Propertys
        public virtual List<Order> Orders { get; set; }
    }
}
