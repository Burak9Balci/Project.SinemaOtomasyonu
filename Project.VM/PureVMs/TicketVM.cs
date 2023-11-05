using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class TicketVM
    {
        public int ID { get; set; }
        public string SeatNumber { get; set; }
        public decimal? Price { get; set; }
        public string SessionTime { get; set; }
        public DateTime TicketDate { get; set; }
    }
}
