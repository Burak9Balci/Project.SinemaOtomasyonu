using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {

        CustomerRepository _cRep;
        public HomeController()
        {
            _cRep = new CustomerRepository();
        }
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CustomerVM customer)
        {
            // Customer aa = _cRep.FirstOrDefault(x => x.Email == customer.Email);
            //  DanteCrypto.CrypHell(aa.PassWord);
            //  customer.PassWord = aa.PassWord;
            if (_cRep.Any(x => x.Email == customer.Email && x.Active == true && (x.CustomerCategory == CustomerRole.Student || x.CustomerCategory == CustomerRole.Full)))
            {
                if (customer.PassWord == DanteCrypto.SifreBoz(_cRep.FirstOrDefault(x => x.Email == customer.Email).PassWord))
                {
                    Session["customer"] = _cRep.FirstOrDefault(x => x.Email == customer.Email);
                    return RedirectToAction("MovieList", "TicketShopping");
                }
                TempData["yanlissifre"] = "Böyle bir sifre yok";
                return View();
            }
            else if (_cRep.Any(x => x.VipID == customer.VipID && x.CustomerCategory == CustomerRole.Vip))
            {
                if (customer.PassWord == DanteCrypto.SifreBoz(_cRep.FirstOrDefault(x =>x.VipID == customer.VipID).PassWord))
                {
                    Session["vip"] = _cRep.FirstOrDefault(x => x.VipID == customer.VipID);
                    return RedirectToAction("MovieList", "TicketShopping");
                }
                TempData["yanlissifre"] = "Böyle bir sifre yok";
                return View();
            }
            TempData["kullaniciyok"] = "Böyle bir Kullanici yok";
            return View();
            
        }
    }
}