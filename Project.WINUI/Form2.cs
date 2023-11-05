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

        
    }
}
