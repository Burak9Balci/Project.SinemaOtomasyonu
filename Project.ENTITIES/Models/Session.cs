using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Session : BaseEntity
    {
        public override string ToString()
        {
            return SessionTime.ToString();
        }

        public Session()
        {
            
        }
        public string SessionTime { get; set; }
        public int TotalRatio { get; set; }
        public int? MovieID { get; set; }
        public int? SalonID { get; set; }
        // Relational Property
        public virtual Movie Movie { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public virtual List<Reservation> Rezervasyons { get; set; }
    }
}
