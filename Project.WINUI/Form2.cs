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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WINUI
{
    public partial class Form2 : Form
    {
        #region Repository
        EmployeeRepository _eRep;
        CustomerRepository _cRep;
        MovieRepository _mRep;
        OrderRepository _oRep;
        ReservationRepository _rRep;
        SalonRepository _sRep;
        SessionRepository _seansRep;
        TicketRepository _tRep;
        OrderDetailRepository _oDDetail;
        #endregion
        public Form2(int employeeID)
        {
            InitializeComponent();
            #region Repository Instances
            _eRep = new EmployeeRepository();
            _cRep = new CustomerRepository();
            _tRep = new TicketRepository();
            _seansRep = new SessionRepository();
            _sRep = new SalonRepository();
            _rRep = new ReservationRepository();
            _oRep = new OrderRepository();
            _mRep = new MovieRepository();
            _oDDetail = new OrderDetailRepository();
            #endregion
            cmbEmployee.DataSource = _eRep.Where(x => x.Role == UserRole.Director).ToList();
            _employeeID = employeeID;
            List<string> degiskenler = new List<string>
            {
               "Employee","Customer","Ticket","Seans","Salon","Rezervasyon","Movie","Order","OrderDetail"
            };
            cmbSelect.DataSource = degiskenler;
            cmbSalon1.DataSource = cmbSalon2.DataSource = _sRep.GetNotPassives();
            cmbMovie.DataSource = _mRep.GetNotPassives();
            
        }
        int _employeeID;
        private void btnAccess_Click(object sender, EventArgs e)
        {
            (cmbEmployee.SelectedItem as Employee).Role = UserRole.DiAutomation;
            (cmbEmployee.SelectedItem as Employee).EmployeeID = _employeeID;
            _eRep.Save();
        }
       
        
        
        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelect.SelectedItem is "Employee") dgvBrif.DataSource = _eRep.GetAll();
            if (cmbSelect.SelectedItem is "Customer") dgvBrif.DataSource = _cRep.GetAll();
            if (cmbSelect.SelectedItem is "Ticket") dgvBrif.DataSource = _tRep.GetAll();
            if (cmbSelect.SelectedItem is "Seans") dgvBrif.DataSource = _seansRep.GetAll();
            if (cmbSelect.SelectedItem is "Salon") dgvBrif.DataSource = _sRep.GetAll();
            if (cmbSelect.SelectedItem is "Rezervasyon") dgvBrif.DataSource = _rRep.GetAll();
            if (cmbSelect.SelectedItem is "Movie") dgvBrif.DataSource = _mRep.GetAll();
            if (cmbSelect.SelectedItem is "Order") dgvBrif.DataSource = _oRep.GetAll(); 
            if (cmbSelect.SelectedItem is "OrderDetail") dgvBrif.DataSource = _oDDetail.GetAll();

        }

        private void btnMovie_Click(object sender, EventArgs e)
        {
            if (_mRep.Any(x => x.FilmTitle != txtName.Text))
            {
                if (cmbSalon1.SelectedItem != null)
                {
                    Movie m = new Movie();
                    m.FilmTitle = txtName.Text;
                    m.ReleaseDate = dtpRelease.Value.Date;
                    m.TakedownDate = dtpTakeDown.Value.Date;
                    m.ImagePath = txtImage.Text;
                    m.Salon = _sRep.Find((cmbSalon1.SelectedItem as Salon).ID);
                    _mRep.Add(m);
                    dgvBrif.DataSource = _mRep.GetAll();
                    MessageBox.Show("Basarılı bir şekilde Eklenmiştir", "Basarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ComboBox boş deger gir");
                }
            }
            else
            {
                MessageBox.Show("Bu Film Sinemaya Daha onceden Eklenmiş");
            }
            
           
        }
        private void Engelle()
        {
            MessageBox.Show("Lutfen bir Sıra seciniz");
        }
        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            if (dgvBrif.SelectedRows.Count > 0)
            {
                (dgvBrif.SelectedRows[0].DataBoundItem as Movie).EmployeeID = _employeeID;
                (dgvBrif.SelectedRows[0].DataBoundItem as Movie).FilmTitle = txtName.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Movie).ReleaseDate = dtpRelease.Value.Date;
                (dgvBrif.SelectedRows[0].DataBoundItem as Movie).TakedownDate = dtpRelease.Value.Date;
                (dgvBrif.SelectedRows[0].DataBoundItem as Movie).Salon = _sRep.Find((cmbSalon1.SelectedItem as Salon).ID);
                (dgvBrif.SelectedRows[0].DataBoundItem as Movie).ImagePath = txtImage.Text;
                _mRep.Update(dgvBrif.SelectedRows[0].DataBoundItem as Movie);
                dgvBrif.DataSource = _mRep.GetAll();
                MessageBox.Show("Basarılı bir şekilde Guncellendi", "Basarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Engelle();
            }
        }
        private void btnSeans_Click(object sender, EventArgs e)
        {
            if (_seansRep.Any(x => x.SessionTime != txtSessionTime.Text))
            {
                if (cmbSalon2.SelectedItem != null && cmbMovie.SelectedItem != null)
                {
                    Session s = new Session();
                    s.Movie = _mRep.Find((cmbMovie.SelectedItem as Movie).ID);
                    s.Salon = _sRep.Find((cmbSalon2.SelectedItem as Salon).ID);
                    s.SessionTime = txtSessionTime.Text;
                    _seansRep.Add(s);
                    dgvBrif.DataSource = _seansRep.GetAll();
                    MessageBox.Show("Basarılı bir şekilde Eklenmiştir","Basarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ComboBoxlardan biri boş Deger giriniz");
                }
            }
            else
            {
                MessageBox.Show("Boyle bir Seans Mevcut");
            }
            
         
        }
        private void btnUpdateSeans_Click(object sender, EventArgs e)
        {
            if (dgvBrif.SelectedRows.Count > 0)
            {
                (dgvBrif.SelectedRows[0].DataBoundItem as Session).SessionTime = txtSessionTime.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Session).Salon = _sRep.Find((cmbSalon2.SelectedItem as Salon).ID);
                (dgvBrif.SelectedRows[0].DataBoundItem as Session).Movie = _mRep.Find((cmbMovie.SelectedItem as Movie).ID);
                _seansRep.Update(dgvBrif.SelectedRows[0].DataBoundItem as Session);
                MessageBox.Show("Basarılı bir şekilde Guncellendi", "Basarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBrif.DataSource = _seansRep.GetAll();
            }
            else
            {
                Engelle();
            }
        }

        private void btnSalon_Click(object sender, EventArgs e)
        {
            if (_sRep.Any(x =>x.SalonNo != txtSalonNo.Text))
            {
                Salon s = new Salon();
                s.Capacity = Convert.ToInt32(txtSalonCap.Text);
                s.SalonNo = txtSalonNo.Text;
                _sRep.Add(s);
                dgvBrif.DataSource = _sRep.GetAll();
                MessageBox.Show("Basarılı bir şekilde Eklenmiştir", "Basarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Boyle bir salon var");
            }
            
        }
        private void btnUpdateSalon_Click(object sender, EventArgs e)
        {
            if (dgvBrif.SelectedRows.Count > 0)
            {
                (dgvBrif.SelectedRows[0].DataBoundItem as Salon).Capacity = Convert.ToInt32(txtSalonCap.Text);
                (dgvBrif.SelectedRows[0].DataBoundItem as Salon).SalonNo = txtSalonNo.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as Salon).EmployeeID = _employeeID;
                _sRep.Update((dgvBrif.SelectedRows[0].DataBoundItem as Salon));
                MessageBox.Show("Basarılı bir şekilde Guncellendi", "Basarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBrif.DataSource = _sRep.GetAll();
            }
            else
            {
                Engelle();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBrif.SelectedRows.Count > 0)
            {
                (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).CancelReason = txtReson.Text;
                (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).EmployeeID = _employeeID;
                (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).DeletedDate = DateTime.Now;
                (dgvBrif.SelectedRows[0].DataBoundItem as BaseEntity).Status = DataStatus.Deleted;
                txtReson.Text = string.Empty;
                MessageBox.Show("Islem tamam bir Sayfa yenileme gerekli");
                _cRep.Save();
            }
            else
            {
                Engelle();
            }
        }
    }
}
