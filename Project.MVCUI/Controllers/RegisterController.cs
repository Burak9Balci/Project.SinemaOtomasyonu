using Project.BLL.Repositories.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        OrderRepository _oRep;
        CustomerRepository _cRep;
        public RegisterController()
        {
            _oRep = new OrderRepository();
            _cRep = new CustomerRepository();
        }
        // GET: Register
        public ActionResult RegisterNow()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNow(CustomerVM customer)
        {
            if (_cRep.Any(x => x.UserName != customer.UserName && x.Email != customer.Email && x.PhoneNumber != customer.PhoneNumber))
            {
                if (customer.PassWord != null && customer.Email != null && (customer.Email.Contains("@gmail.com") || customer.Email.Contains("@hotmail.com") || customer.Email.Contains("@windowslive.com")))
                {
                    Customer c = new Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        UserName = customer.UserName,
                        PassWord = DanteCrypto.Sifrele(customer.PassWord),
                        Email = customer.Email,
                        Age = customer.Age,
                        Gender = customer.Gender,
                        Job = customer.Job,
                        CustomerCategory = customer.CustomerRole

                    };
                    _cRep.Add(c);
                    if (c.CustomerCategory == CustomerRole.Vip)
                    {
                        return RedirectToAction("VipOrder", c);
                    }
                    string mailbody = "Tekbrikler Hesabınıx olusturuldu hesabinizi aktive etmek için http://localhost:57382/Register/Activation/" + c.ActivationCode + " linkine tıklaya bilirsiniz";
                    MailService.Send(customer.Email, body: mailbody, subject: "Hesap Aktivasyon");
                    return View("RegisterOK");
                }
                else
                {
                    TempData["kullanıcımevcut"] = "Bilgiler Eksik";
                    return View();
                }
                
            }
            TempData["kullanıcımevcut"] = "Boyle bir kullanıcı mevcut";
            return View();
            
        }
        public ActionResult RegisterOK()
        {
            return View();
        }
        public ActionResult Activation(Guid id) 
        {
            Customer aktifEdilicek = _cRep.FirstOrDefault(x =>x.ActivationCode ==id);
            if (aktifEdilicek != null)
            {
                aktifEdilicek.Active = true;
                _cRep.Update(aktifEdilicek);
                TempData["aktifMi"] = "Hesap Aktif";
                return RedirectToAction("Login","Home");
            }
            TempData["aktifMi"] = "Hesap Aktif Degil";
            return RedirectToAction("Login","Home");
        }
        public ActionResult OrderOk()
        {
            return View();
        }
        public ActionResult VipOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VipOrder(OrderPageVM orderPage, Customer c)
        {
            orderPage.PaymentRequestModel.ShoppingPrice = 200;
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
                    return View();
                }
                if (result.IsSuccessStatusCode) bResult = true;
                else bResult = false;
                if (bResult)
                {
                    Order o = new Order();
                    o.CustomerID = c.ID;
                    o.OrderType = OrderType.Internet;
                    o.MailAddress = c.Email;
                    o.TotalPrice = orderPage.PaymentRequestModel.ShoppingPrice;
                    c.Active = c.VipPayed = c.Monthly = true;
                    c.FriendPayed = 2;
                    c.VipID = Guid.NewGuid().ToString();
                    c.MonthlyDate = DateTime.Now;
                    _oRep.Add(o);
                    _cRep.Update(c);
                    return RedirectToAction("OrderOk");

                }
                else
                {
                    TempData["sorun"] = "bilgiler yanlıs";
                    return View();
                }
                
            };      
            
        }
    }
}