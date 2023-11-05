using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class ListSeatPageVM
    {
        public List<SessionVM> Sessions { get; set; }
        public SessionVM Session { get; set; }
        public TicketVM Ticket { get; set; }
        public MovieVM Movie { get; set; }
        public ReservationVM Reservation { get; set; }
        public List<TicketVM> Tickets { get; set; }
        public List<ReservationVM> Reservations { get; set; }
    }
}