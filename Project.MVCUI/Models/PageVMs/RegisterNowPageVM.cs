using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class RegisterNowPageVM
    {
        public CustomerVM Customer { get; set; }
        public List<CustomerVM> Customers { get; set; }

    }
}