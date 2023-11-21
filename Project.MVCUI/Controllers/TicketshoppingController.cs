using Microsoft.Ajax.Utilities;
using PagedList;
using Project.BLL.Repositories.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.PageVMs;
using Project.MVCUI.OuterRequestTool;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class TicketshoppingController : Controller
    {
        // GET: Automation
        SessionRepository _sRep;
        MovieRepository _mRep;
        ReservationRepository _rRep;
        OrderRepository _oRep;
        TicketRepository _tRep;
        OrderDetailRepository _oDetailRep;
        SalonRepository _saRep;
        public TicketshoppingController()
        {
            _oRep = new OrderRepository();
            _sRep = new SessionRepository();
            _rRep = new ReservationRepository();
            _mRep = new MovieRepository();
            _tRep = new TicketRepository();
            _oDetailRep = new OrderDetailRepository();
            _saRep = new SalonRepository();
        }
        public ActionResult MovieList() //
        {
            ListMoviePageVM l = new ListMoviePageVM
            {
                Movies = NotPassivTicketsMovies().Where(x =>x.ReleaseDate.Date <= DateTime.Now.Date && x.TakedownDate.Date >= DateTime.Now.Date).ToList(),
            };
            return View(l);
        }
        public ActionResult TakeTicket(string title)
        {
            return ListSeatPageVM(title);
        }
        [HttpPost]
        public ActionResult TakeTicket(string filmTitle,TicketVM ticket)
        {
            if (_tRep.Any(x => x.SeatNumber == ticket.SeatNumber && x.SessionID == ticket.ID) || _rRep.Any(x => x.ResSeatNo == ticket.SeatNumber && x.SessionID == ticket.ID))
            {
                TempData["var"] = "Sectiğiniz koltuk dolu";
                return ListSeatPageVM(filmTitle);
            }
            else if (ticket.TicketDate.Date >= DateTime.Now.Date)
            {
                if (Session["vip"] != null && ticket.TicketDate.Date <= DateTime.Now.AddDays(7).Date && ticket.SessionTime != null)
                {
                    Ticket t = new Ticket();
                    t.Session = _sRep.FirstOrDefault(x =>x.SessionTime == ticket.SessionTime);
                    t.SeatNumber = ticket.SeatNumber;
                    t.Type = TicketType.Ticket;
                    t.Session.TotalRatio++;
                    t.TicketDate = ticket.TicketDate.Date;
                    _tRep.Add(t);
                    return RedirectToAction("ConfirmOrder", t);
                }
                else if (ticket.TicketDate.Date <= DateTime.Now.AddDays(2).Date && ticket.SessionTime != null)
                {
                    Ticket t = new Ticket();
                    Session["session"] = t.Session = _sRep.FirstOrDefault(x => x.SessionTime == ticket.SessionTime);
                    t.SeatNumber = ticket.SeatNumber;
                    t.Type = TicketType.Ticket;
                    t.Session.TotalRatio++;
                    t.TicketDate = ticket.TicketDate.Date;
                    _tRep.Add(t);
                    return RedirectToAction("ConfirmOrder", t);
                }
                TempData["vipbug"] = Session["vip"] != null ? "You can buy tickets at least 7 days in advance" : null;
                TempData["bug"] = ticket.SessionTime== null ? "Select a session " : "You can buy tickets at least 2 days in advance";
                return RedirectToAction("TakeTicket", new { title = filmTitle });
            }
            TempData["time"] = "You can buy tickets at least 2 days in advance";
            return ListSeatPageVM(filmTitle);


        }
        public ActionResult MakeReservation(string title)
        {
            return ListSeatPageVM(title);
        }
        [HttpPost]
        public ActionResult MakeReservation(string filmTitle, ReservationVM reservation)
        {
            if (reservation.ID != 0 && reservation.SessionTime.Date <= DateTime.Now.AddDays(2).Date)
            {
                if (_tRep.Any(x => x.SeatNumber == reservation.ResSeatNumber && x.SessionID == reservation.ID) || _rRep.Any(x => x.ResSeatNo == reservation.ResSeatNumber && x.SessionID == reservation.ID))
                {
                    TempData["var"] = "Sectiğiniz koltuk dolu";
                    return ListSeatPageVM(filmTitle);
                }
                else
                {
                    Reservation r = new Reservation();
                    r.Session = _sRep.Find(reservation.ID);
                    r.Type = TicketType.Rez;
                    r.Customer = Session["customer"] as Customer != null ? Session["customer"] as Customer : Session["vip"] as Customer;
                    r.ResSeatNo = reservation.ResSeatNumber;
                    r.SessionID = reservation.SessionID;
                    r.TicketTime = reservation.SessionTime.Date;
                    _rRep.Add(r);
                    return RedirectToAction("ReservationInfo");
                }
               
            }
            TempData["badsession"] = "Select a session ";
            return RedirectToAction("MakeReservation", new { title = filmTitle });
        }
        public ActionResult ReservationInfo()
        {
            return View();
        }
        public ActionResult OrderInfo()
        {
            return View();
        }
        public ActionResult ConfirmOrder()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult ConfirmOrder(OrderPageVM orderPage,Ticket t)
        {
            if (orderPage.Save == true)
            {
                orderPage.PaymentRequestModel = (Session["kart"] as OrderPageVM).PaymentRequestModel;
            }
            if ((Session["vip"] as Customer) != null)
            {
                if (DateTime.Now > (Session["vip"] as Customer).MonthlyDate.AddMonths(1))
                {
                    (Session["vip"] as Customer).Monthly = true;
                    (Session["vip"] as Customer).FriendPayed = 2;
                }
            }
            if ((Session["vip"] as Customer) != null)
            {
                if ((Session["vip"] as Customer).Monthly == true)
                {
                    orderPage.PaymentRequestModel.ShoppingPrice = t.Price = 0;
                    (Session["vip"] as Customer).Monthly = false;
                    (Session["vip"] as Customer).MonthlyDate = DateTime.Now;
                    return OrderTicketMVC(orderPage, t);
                }
                else if ((Session["vip"] as Customer).FriendPayed != 0)
                {

                    (Session["vip"] as Customer).FriendPayed--;
                    orderPage.PaymentRequestModel.ShoppingPrice = t.Price = 8 * t.Price / 10;
                    return OrderTicketMVC(orderPage, t);
                }
                orderPage.PaymentRequestModel.ShoppingPrice = t.Price = t.Price / 2;
                return OrderTicketMVC(orderPage, t);
            }
            else
            {
                if ((Session["customer"] as Customer).CustomerCategory == CustomerRole.Full)
                {
                    if ((Session["session"] as Session).SessionTime == _mRep.Find(_sRep.Find((Session["session"] as Session).ID).Movie.ID).Seanses[0].SessionTime || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        orderPage.PaymentRequestModel.ShoppingPrice = t.Price = t.Price / 2;
                        return OrderTicketMVC(orderPage, t);
                    }
                    orderPage.PaymentRequestModel.ShoppingPrice = t.Price;
                    return OrderTicketMVC(orderPage, t);
                }
                else if ((Session["customer"] as Customer).CustomerCategory == CustomerRole.Student)
                {
                    if ((Session["session"] as Session).SessionTime == _mRep.Find(_sRep.Find((Session["session"] as Session).ID).Movie.ID).Seanses[0].SessionTime || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        orderPage.PaymentRequestModel.ShoppingPrice = t.Price = t.Price / 2;
                        return OrderTicketMVC(orderPage, t);
                    }
                    orderPage.PaymentRequestModel.ShoppingPrice = t.Price = 6 * t.Price / 10;
                    return OrderTicketMVC(orderPage, t);
                }
                return View();
            }
            
        }
       
        private ActionResult OrderTicketMVC(OrderPageVM orderPage,Ticket t)
        {
            bool bResult;
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:58604/api/");
                Task<HttpResponseMessage> response = client.PostAsJsonAsync("Payment/RecivePayment", orderPage.PaymentRequestModel);
                HttpResponseMessage result;
                try
                {
                    result = response.Result;
                }
                catch (Exception)
                {

                    TempData["baglantiRed"] = "Banka baglantiyi Reddetti";
                    return RedirectToAction("MovieList");
                }
                if (result.IsSuccessStatusCode) bResult = true;
                else bResult = false;
                if (bResult)
                {
                    Order o = new Order();
                    o.OrderType = OrderType.Internet;
                    if (Session["customer"] as Customer != null)
                    {
                        o.Customer = Session["customer"] as Customer;
                        o.MailAddress = (Session["customer"] as Customer).Email;
                    }
                    else
                    {
                        o.Customer = Session["vip"] as Customer;
                        o.MailAddress = (Session["vip"] as Customer).Email;
                    }
                    o.TotalPrice = orderPage.PaymentRequestModel.ShoppingPrice;
                    t.Active = true;
                    _tRep.Update(t);
                    _oRep.Add(o);
                    OrderDetail od = new OrderDetail();
                    od.Order = o;
                    od.Ticket = t;
                    od.TotalPrice = orderPage.PaymentRequestModel.ShoppingPrice;
                    _oDetailRep.Add(od);
                    MailService.Send(o.MailAddress, subject: "Bilet", body: $"biletiniz başarıyla alınmıştır {o.TotalPrice}₺ tutarında odeme yapilmiştir");
                    return RedirectToAction("OrderInfo");
                }
                else
                {
                 
                    TempData["sorun"] = "bilgiler yanlıs";
                    return View();
                }

            }
        }
        private ActionResult ListSeatPageVM(string title)
        {
            ListSeatPageVM l = new ListSeatPageVM
            {

                Sessions = _sRep.Where(x => x.Movie.FilmTitle == title).Select(x => new SessionVM
                {
                    ID = x.ID,
                    SessionTime = x.SessionTime,
                    SessionSalon = x.Salon.SalonNo

                }).ToList(),
                Movie = _mRep.Select(x => new MovieVM
                {
                    FilmTitle = title,
                    SalonNo = x.Salon.SalonNo

                }).FirstOrDefault(),
                Tickets = _tRep.Where(x => x.Status != DataStatus.Deleted).Select(x => new TicketVM
                {
                    SeatNumber = x.SeatNumber,
                    SessionTime = x.Session.SessionTime,
                    TicketDate = x.TicketDate.Date
                }).ToList(),
                Reservations = _rRep.Where(x => x.Status != DataStatus.Deleted).Select(x => new ReservationVM
                {
                    SessionTime = Convert.ToDateTime(x.Session.SessionTime),
                    ResSeatNumber = x.ResSeatNo

                }).ToList()
            };
            return View(l);
        }
        private List<MovieVM> NotPassivTicketsMovies()
        {
            List<MovieVM> movies = _mRep.Where(x => x.Status != DataStatus.Deleted).Select(x => new MovieVM
            {
                FilmTitle = x.FilmTitle,
                ReleaseDate = x.ReleaseDate,
                TakedownDate = x.TakedownDate,
                ImagePath = x.ImagePath,
                SalonNo = x.Salon.SalonNo

            }).ToList();
            return movies;
        }
        
    }
}