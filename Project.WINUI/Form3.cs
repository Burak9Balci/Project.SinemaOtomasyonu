using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WINUI
{
    public partial class Form3 : Form
    {
        #region Repository
        CustomerRepository _cRep;
        MovieRepository _mRep;
        OrderRepository _oRep;
        ReservationRepository _rRep;
        SalonRepository _sRep;
        SessionRepository _seRep;
        TicketRepository _tRep;
        #endregion
        int _employeeID;
        public Form3(int employeeID)
        {
            InitializeComponent();
            #region Repository Instanceler
            _cRep = new CustomerRepository();
            _tRep = new TicketRepository();
            _seRep = new SessionRepository();
            _sRep = new SalonRepository();
            _rRep = new ReservationRepository();
            _oRep = new OrderRepository();
            _mRep = new MovieRepository();
            #endregion 
            _employeeID = employeeID;
            List<string> degiskenler = new List<string>
            {
               "Reservation","Movie","Customer","Ticket","Order","Session","Salon"
            };
            List<CustomerRole> roles = new List<CustomerRole>
            {
                CustomerRole.Full , CustomerRole.Student
            };
            cmbRoles.DataSource = roles;             
            cmbSelect.DataSource = degiskenler;
        }
        private void cmbSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            //Combo boxdan sectiği değeri DataGridView e Yazdırır ve sectiği değer ile alaklı butonları aktif eder alakası olmayan butonları diaktif eder
            if (cmbSelect.SelectedItem is "Movie") dtpTime_ValueChanged(sender, e);
            if (cmbSelect.SelectedItem is "Session") dtpTime_ValueChanged(sender, e); 
            if (cmbSelect.SelectedItem is "Reservation") dgvBrif.DataSource = _rRep.GetAll(); //Reservation
            if (cmbSelect.SelectedItem is "Ticket") dgvBrif.DataSource = _tRep.GetAll();
            if (cmbSelect.SelectedItem is "Salon") dgvBrif.DataSource = _sRep.GetAll();
            if (cmbSelect.SelectedItem is "Customer") dgvBrif.DataSource = _cRep.GetAll();
            if (cmbSelect.SelectedItem is "Order") dgvBrif.DataSource = _oRep.GetAll();
            if (cmbSelect.SelectedItem is "Customer")
            {
                btnAdd.Enabled = btnUpdate.Enabled = btnCustomer.Enabled = true;
                return;
            }
            btnAdd.Enabled = btnUpdate.Enabled = btnRezAktif.Enabled = btnCustomer.Enabled=false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //eger data basede textboxlara girilen değerlerin oldugu bir customer yoksa bir customer yaratır ve textboxlardan gelen bilgiler ile kaydeder,eger
            //textboxa girilen değerlerden ayırt edici olan özellik data basede varsa farklı bir değer girilmesini ister
            if (_cRep.Any(x => x.UserName != txtUserName.Text && x.Email != txtEmail.Text && x.PhoneNumber != txtPhoneNumber.Text))
            {
                if (cmbRoles.SelectedItem != null)
                {
                    Customer c = new Customer
                    {
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        PhoneNumber = txtPhoneNumber.Text,
                        UserName = txtUserName.Text,
                        PassWord = DanteCrypto.Sifrele(txtPassWord.Text),
                        Gender = rdoGirl.Checked ? "Female" : "Male",
                        Age = txtAge.Text,
                        EmployeeID = _employeeID,
                        Active = true,
                        Job = txtJob.Text,
                        CustomerCategory = (CustomerRole)cmbRoles.SelectedItem

                    };
                    _cRep.Add(c);
                    dgvBrif.DataSource = _cRep.GetAll();
                    Clean();
                    return;
                }
                MessageBox.Show("Select a Role");
                return;
            }
            MessageBox.Show("This Human in your Database");
            return;
        } 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Data basede kayitli olan değeri textboxlardan alarak olmasını istediği değeri Atar
            if (dgvBrif.SelectedRows.Count > 0)
            {
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).FirstName = txtFirstName.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).LastName = txtLastName.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Email = txtEmail.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).PhoneNumber = txtPhoneNumber.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).UserName = txtUserName.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).PassWord = txtPassWord.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Gender = rdoGirl.Checked ? "Female" : "Male";
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Age = txtAge.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).EmployeeID = _employeeID;
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Job = txtJob.Text;
                _cRep.Update((dgvBrif.SelectedRows[0].DataBoundItem as Customer));
                dgvBrif.DataSource = _cRep.GetAll();
                return;
            }
            MessageBox.Show("Please first select a Customer");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Secili olan Classı Pasife Alır ve Textboxdan boxdan nedenini yazdırır
            if (dgvBrif.SelectedRows.Count > 0)
            {
                if (txtReson.Text != "")
                {
                    (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).CancelReason = txtReson.Text;
                    (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).EmployeeID = _employeeID;
                    (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).DeletedDate = DateTime.Now;
                    (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).Status = DataStatus.Deleted;
                    MessageBox.Show("It is DONE Need Refresh");
                    txtReson.Text = string.Empty;
                    _cRep.Save();
                    return;
                }
                MessageBox.Show("Pls give the reson for Delete");
                return;
            }
            MessageBox.Show("Select a Row");
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            // TextBOxa Vıp olan employee nin VipIdsi girip Bilgilerini DataGridViewda gösterir
            
            if (txtID.Text != "")
            {
                if (_cRep.Any(x => x.VipID == txtID.Text))
                {
                    dgvBrif.DataSource = _cRep.GetAll().Where(x => x.VipID == txtID.Text).ToList();
                    _c.VipID = _cRep.FirstOrDefault(x => x.VipID == txtID.Text).VipID;
                    txtID.Text = string.Empty;
                    return;
                }
                MessageBox.Show("Need VipID");
                return;
            }
            MessageBox.Show("There is not VIP User with That ID");
        }
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            // Customer ın CAtegorysini değiştir eger customer vip is ip ozelliklerini kaldırır
            if (dgvBrif.SelectedRows.Count > 0 && dgvBrif.SelectedRows[0].DataBoundItem is Customer)
            {          
                (dgvBrif.SelectedRows[0].DataBoundItem as Customer).CustomerCategory = (CustomerRole)cmbRoles.SelectedItem;
                if ((dgvBrif.SelectedRows[0].DataBoundItem as Customer).VipID != null)
                {
                    (dgvBrif.SelectedRows[0].DataBoundItem as Customer).VipID = null;
                    (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Monthly = false;
                    (dgvBrif.SelectedRows[0].DataBoundItem as Customer).VipPayed = false;
                }
                _cRep.Update((dgvBrif.SelectedRows[0].DataBoundItem as Customer));
                dgvBrif.DataSource = _cRep.GetAll();
                return;
            }
            MessageBox.Show("Please first select a Customer");
        }
        private void btnRezAktif_Click(object sender, EventArgs e)
        {
            //Secili olan Rezervasyonu aktif eder Işlemi yapan Employee yi belirtir  ardından Update eder
            if (dgvBrif.SelectedRows.Count > 0 && dgvBrif.SelectedRows[0].DataBoundItem is Reservation)
            {
                (dgvBrif.SelectedRows[0].DataBoundItem as Reservation).Active = true;
                (dgvBrif.SelectedRows[0].DataBoundItem as Reservation).EmployeeID = _employeeID;
                (dgvBrif.SelectedRows[0].DataBoundItem as Reservation).Type = TicketType.Ticket;
                _rRep.Update(_rRep.Find((dgvBrif.SelectedRows[0].DataBoundItem as Reservation).ID));
                dgvBrif.DataSource = _rRep.GetAll();
            
                btnRezAktif.Enabled = false;
                return;
            }
            MessageBox.Show("Please first select a Reservation");
            
        }
        Customer _c = new Customer();
        private void dgvBrif_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewda secili olan Rezervasyonu aktif etmek için tıklanır ardından aktif etme butonu aktif olunur
            if (dgvBrif.SelectedRows != null && cmbSelect.SelectedItem is "Reservation") btnRezAktif.Enabled = true;
            if (dgvBrif.SelectedRows.Count > 0 && cmbSelect.SelectedItem is "Customer")
            {
                txtFirstName.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).FirstName;
                txtLastName.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).LastName ;
                txtEmail.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Email;
                txtPhoneNumber.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).PhoneNumber;
                txtUserName.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).UserName;
                txtPassWord.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).PassWord;
                txtAge.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Age;
                _employeeID = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).EmployeeID;
                txtJob.Text = (dgvBrif.SelectedRows[0].DataBoundItem as Customer).Job;
              
            }
        }
        
        private void dgvBrif_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Secili olan Movieye 2 kere tıklanırsa secili olan movienin bilet ekranını gösterir 
            if (dgvBrif.SelectedRows[0].DataBoundItem is Movie) SalonListele(); 

            if (dgvBrif.SelectedRows != null && cmbSelect.SelectedItem is "Ticket") 
            {
                // Secili olan Ticketa 2 kere tıklanırsa secili olan Ticketın odeme ekranını gösterir
                Form4 form = new Form4(dgvBrif.SelectedRows[0].DataBoundItem as Ticket, _c.VipID, _employeeID,(dgvBrif.SelectedRows[0].DataBoundItem as Ticket).SessionID);
                form.ShowDialog();
                dgvBrif.DataSource = _tRep.GetAll();
                return;
            }
            if (dgvBrif.SelectedRows != null && cmbSelect.SelectedItem is "Customer" && (dgvBrif.SelectedRows[0].DataBoundItem as Customer).CustomerCategory != CustomerRole.Vip)               
            {
                // Secili olan customera 2 kere tıklanırsa Secili olan customer ın Customer ROle ını odeme karşılığında Vip yapar tabi
                Form4 form = new Form4(dgvBrif.SelectedRows[0].DataBoundItem as Ticket, (dgvBrif.SelectedRows[0].DataBoundItem as Customer).VipID?? (dgvBrif.SelectedRows[0].DataBoundItem as Customer).UserName, _employeeID, (dgvBrif.SelectedRows[0].DataBoundItem as Ticket).SessionID);
                form.ShowDialog();
                _cRep.Update(_cRep.Find((dgvBrif.SelectedRows[0].DataBoundItem as Customer).ID));
                dgvBrif.DataSource = _cRep.GetAll();
            } 
        }
        private void dtpTime_ValueChanged(object sender, EventArgs e)
        {
            if (cmbSelect.SelectedItem is "Movie")
            {
                foreach (Movie item in _mRep.GetNotPassives())
                {                    
                    dgvBrif.DataSource = _mRep.GetNotPassives().Where(x =>x.ReleaseDate.Date <= dtpTime.Value.Date && x.TakedownDate.Date >= dtpTime.Value.Date).ToList();          
                }
            }
            else if (cmbSelect.SelectedItem is "Session")
            {
                foreach (Movie movie in _mRep.GetNotPassives())
                {
                    foreach (Session session in movie.Seanses)
                    {        
                        if (movie.ReleaseDate.Date <= dtpTime.Value.Date && movie.TakedownDate.Date >= dtpTime.Value.Date)
                        {
                            dgvBrif.DataSource = _seRep.GetNotPassives();
                            return;
                        }
                        dgvBrif.DataSource = null;
                    }
                }

            }
        }
        /// <summary>
        /// Ticket Almak istediğimiz Filme 2 kere tıkladıgımızda  bizi o filmin salonun formuna yönlendirir ve EmployeeID Film in Adını ve Film ın salonunun no sunu yolluyoruz
        /// </summary>
        private void SalonListele()
        {
            switch ((dgvBrif.SelectedRows[0].DataBoundItem as Movie).SalonID)
            {
                case 1:
                    Salon1 s1 = new Salon1(_employeeID,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).FilmTitle,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).Salon.ID);
                    s1.ShowDialog();
                    break;
                case 2:
                    Salon2 s2 = new Salon2(_employeeID,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).FilmTitle,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).Salon.ID);
                    s2.ShowDialog();
                    break;
                case 3:
                    Salon3 s3 = new Salon3(_employeeID,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).FilmTitle,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).Salon.ID);
                    s3.ShowDialog();
                    break;
                case 4:
                    Salon4 s4 = new Salon4(_employeeID,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).FilmTitle,(dgvBrif.SelectedRows[0].DataBoundItem as Movie).Salon.ID);
                    s4.ShowDialog();
                    break;  
            }
        }
        /// <summary>
        /// Tab Indexi literal olarak verilen degere eşit olan TextBoxların Texitini temizler
        /// </summary>
        private void Clean()
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox && item.TabIndex == 1)
                {
                    item.Text = string.Empty;
                    cmbRoles.SelectedItem = null;
                    if (rdoGirl.Checked)
                    {
                        rdoGirl.Checked = false;
                    }
                    else if (rdoMale.Checked) 
                    {
                        rdoMale.Checked = false;
                    }
                }
            } 
        }

        
    }
}
