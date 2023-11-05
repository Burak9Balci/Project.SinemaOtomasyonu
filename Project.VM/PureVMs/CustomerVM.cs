using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class CustomerVM : AppUserVM
    {
        public bool Active { get; set; }
        public string Job { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string VipID { get; set; }
        public CustomerRole CustomerRole { get; set; }

    }
}
