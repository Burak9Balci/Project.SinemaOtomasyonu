﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class OrderDetail : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public short Quantity { get; set; }
        public int? TicketID { get; set; }
        public int? OrderID { get; set; }
        //Relational Property
        public virtual Ticket Ticket { get; set; }
        public virtual Order Order { get; set; }
    }
}
