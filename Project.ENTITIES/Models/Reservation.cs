using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Reservation : BaseEntity
    {
        public string ResSeatNo { get; set; }
        public bool Active { get; set; }
        public DateTime TicketTime { get; set; }
        public TicketType Type { get; set; }
        public int CustomerID { get; set; }
        public int SessionID { get; set; }
        //Relational Property
        public Customer Customer { get; set; }
        public Session Session { get; set; }
    }
}
