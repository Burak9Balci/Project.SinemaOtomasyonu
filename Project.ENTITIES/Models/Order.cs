using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order : BaseEntity
    {
        
        public string MailAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderType OrderType { get; set; }
        public int? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        //Relational Property
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
