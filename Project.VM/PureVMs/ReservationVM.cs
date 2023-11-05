using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class ReservationVM
    {
        public int ID { get; set; }
        public DateTime SessionTime { get; set; }
        public int SessionID { get; set; }
        public string ResSeatNumber { get; set; }
    }
}
