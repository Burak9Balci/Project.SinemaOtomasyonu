using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Rezervasyon : BaseEntity
    {
        public Rezervasyon()
        {
            
        }
        public string RezSeatNo { get; set; }
        public string MovieName { get; set; }
        public string SalonNo { get; set; }
        public bool Active { get; set; }
        public int CustomerID { get; set; }
        public int SessionID { get; set; }
        public TicketType Type { get; set; }
        public DateTime TicketTime { get; set; }
        // Relational Property
        public virtual Session Session { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
