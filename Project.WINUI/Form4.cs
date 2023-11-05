using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using Project.WINUI.OuterRequestModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WINUI
{
    public partial class Form4 : Form
    {
        Ticket _t;
        Customer _c;
        CustomerRepository _cRep;
        TicketRepository _tRep;
        MovieRepository _mRep;
        SessionRepository _sRep;
        int _eId;
        int _sessionID;
        OrderRepository _oRep;
        OrderDetailRepository _oDRep;
        public Form4(Ticket t,string vipID,int eId,int sessionID)
        {
            InitializeComponent();
            _sRep = new SessionRepository();
            _oRep = new OrderRepository();
            _cRep = new CustomerRepository();
            _tRep = new TicketRepository();
            _mRep = new MovieRepository();
            _t = t;
            _eId = eId;
            _sessionID = sessionID;
            _c = _cRep.FirstOrDefault(x =>x.VipID == vipID) == null ? _cRep.FirstOrDefault(x => x.UserName == vipID) : _cRep.FirstOrDefault(x => x.VipID == vipID);
            _oDRep = new OrderDetailRepository();
            
        }
        HttpResponseMessage _result;
        bool _bResult;
        private void btn_ode_Click(object sender, EventArgs e)
        {
            if (_c != null)
            {
                if (DateTime.Now > _c.MonthlyDate.AddMonths(1))
                {
                    _c.Monthly = true;
                    _c.FriendPayed = 2;
                }
                if (rdoStudent.Checked)
                {
                    if (_t.Session.SessionTime == _mRep.Find(_sRep.Find(_sessionID).Movie.ID).Seanses[0].SessionTime || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        _t.Price = _t.Price / 2;
                        OrderTicketForm(_t);
                        this.Close();
                        return;
                    }
                    _t.Price = 6 * _t.Price / 10;
                    OrderTicketForm(_t);
                    this.Close();
                    return;
                }
                else if (rdoFull.Checked)
                {
                    if (_t.Session.SessionTime == _mRep.Find(_sRep.Find(_sessionID).Movie.ID).Seanses[0].SessionTime || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        _t.Price = _t.Price / 2;
                        OrderTicketForm(_t);
                        this.Close();
                        return;
                    }
                    OrderTicketForm(_t);
                    this.Close();
                    return;
                }
                else if (rdoFriend.Checked)
                {
                    if (_c.CustomerCategory == CustomerRole.Vip)
                    {
                        if (_c.FriendPayed != 0)
                        {
                            _t.Price = 8 * _t.Price / 10;
                            _c.FriendPayed--;
                            OrderTicketForm(_t);
                            this.Close();
                            return;
                        }
                        MessageBox.Show("Arkadaş odeme sayini gectin");
                       
                        return;
                    }
                    MessageBox.Show("You are not Vip");
           
                    return;
                }
                else if (rdoVip.Checked)
                {
                    if (_c.CustomerCategory == CustomerRole.Vip)
                    {
                        if (_c.Monthly == true)
                        {
                            Order o = new Order();
                            o.OrderType = OrderType.Automation;
                            o.TotalPrice = 0;
                            o.EmployeeID = _eId;
                            o.CustomerID = _c.ID;
                            _t.Active = true;
                            _t.Price = 0;
                            _c.Monthly = false;
                            _c.MonthlyDate = DateTime.Now;
                            _tRep.Update(_t);
                            _oRep.Add(o);
                            MessageBox.Show("odeme alındı");
                            this.Close();
                            return;
                        }
                        else
                        {

                            _t.Price = _t.Price / 2;
                            OrderTicketForm(_t);
                            this.Close();
                            return;
                        }
                    }
                    MessageBox.Show("You are not Vip");
             
                    return;
                }
                else if (rdoVipPay.Checked)
                {
                    if (_c.VipPayed == false)
                    {
                        VipOrder();
                        this.Close();
                        return;
                    }
                    MessageBox.Show("it is already Vip");
                }
                else
                {
                    MessageBox.Show("Please selecet a Category");
                    return;
                }
            }
            MessageBox.Show("ID giriniz");
            this.Close();
            return;
        }
        /// <summary>
        /// Bilet Order Metodu Bu metot Order yaratır bileti aktif eeder
        /// </summary>
        /// 


        ////
        private void OrderTicketForm(Ticket t)
        {
            PaymentRequestModel paymentRequest = new PaymentRequestModel
            {
                CardUserName = txtName.Text,
                SecurityNumber = txtSecurityNo.Text,
                CardNumber = txtCardNo.Text,
                ShoppingPrice = _t.Price,
                CardExpiryMonth = Convert.ToInt32(txtMounth.Text),
                CardExpiryYear = Convert.ToInt32(txtYear.Text)
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58604/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/RecivePayment", paymentRequest);
                try
                {
                    _result = postTask.Result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Banka ile baglanti kurulamadı");
                    return;
                }
                if (_result.IsSuccessStatusCode) _bResult = true;
                else _bResult = false;
                if (_bResult)
                {
                    Order o = new Order();
                    o.OrderType = OrderType.Automation;
                    o.TotalPrice = _t.Price;
                    o.EmployeeID = _eId;
                    if (_c.CustomerCategory == CustomerRole.Vip)
                    {
                        o.CustomerID = _c.ID;
                    }
                    _t.Active = true;
                    _oRep.Add(o);
                    _tRep.Update(_t);
                    OrderDetail od = new OrderDetail();
                    od.Ticket = t;
                    od.Order = o;
                    od.EmployeeID = _eId;
                    od.TotalPrice = paymentRequest.ShoppingPrice;
                    _oDRep.Add(od);
                    MessageBox.Show("odeme alındı");
                    
                }
                else
                {
                    Task<string> s = _result.Content.ReadAsStringAsync();
                    MessageBox.Show($"{s}");
                }


            }
        }
        /// <summary>
        /// BU metot bir kişiyi Vip Uye yapmak için kullanılır
        /// </summary>
        private void VipOrder()
        {
            PaymentRequestModel paymentRequest = new PaymentRequestModel
            {
                CardUserName = txtName.Text,
                SecurityNumber = txtSecurityNo.Text,
                CardNumber = txtCardNo.Text,
                ShoppingPrice = 200,
                CardExpiryMonth = Convert.ToInt32(txtMounth.Text),
                CardExpiryYear = Convert.ToInt32(txtYear.Text)
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58604/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/RecivePayment", paymentRequest);
                try
                {
                    _result = postTask.Result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Banka ile baglanti kurulamadı");
                    return;
                }
                if (_result.IsSuccessStatusCode) _bResult = true;
                else _bResult = false;
                if (_bResult)
                {
                    Order o = new Order();
                    o.OrderType = OrderType.Automation;
                    o.TotalPrice = paymentRequest.ShoppingPrice;
                    o.EmployeeID = _eId;
                    if (_c != null)
                    {
                        _c.CustomerCategory = CustomerRole.Vip;
                        _c.VipPayed =  _c.Monthly = true;
                        _c.VipID = Guid.NewGuid().ToString();
                        _c.FriendPayed = 2;
                        o.CustomerID = _c.ID;
                    }
                    _oRep.Add(o);
                    MessageBox.Show("odeme alındı");
                }
                else
                {
                    Task<string> s = _result.Content.ReadAsStringAsync();
                    MessageBox.Show($"{s}");
                    
                }


            }
        }

        
    }
}
