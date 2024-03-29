﻿using Project.MVCUI.OuterRequestTool;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class OrderPageVM
    {
        public OrderVM Order { get; set; }
        public PaymentRequestModel PaymentRequestModel { get; set; }
        public bool Save { get; set; }
    }
}