using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class OrderVM
    {
        public string UserName { get; set; }
        public int? UserID { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
