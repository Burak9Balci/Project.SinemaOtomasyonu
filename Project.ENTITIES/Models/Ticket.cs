using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Ticket : BaseEntity
    {
        public Ticket()
        {
            Price = 50;
        }
        public override string ToString()
        {
            return SeatNumber;
        }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
        public int SessionID { get; set; }
        public bool Active { get; set; }
        public TicketType Type { get; set; }
        public DateTime TicketDate { get; set; }
 
        //Relational Property
        public virtual Session Session { get; set; }
        
 
    }
}
