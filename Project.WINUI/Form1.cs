using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _eRep = new EmployeeRepository();      
        }
        EmployeeRepository _eRep;  
       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_eRep.Any(x => x.UserName == txtUserName.Text && (x.Role == UserRole.Admin || x.Role == UserRole.Director)))
            {
                if (txtPassWord.Text == DanteCrypto.SifreBoz(_eRep.FirstOrDefault(x =>x.UserName == txtUserName.Text).PassWord))
                {
                    Form2 form2 = new Form2(_eRep.FirstOrDefault(x => x.UserName == txtUserName.Text).ID);
                    form2.ShowDialog();
                    return;
                }
                MessageBox.Show("sifre Yanlis");
                return;
            }
            else if (_eRep.Any(x => x.UserName == txtUserName.Text && (x.Role == UserRole.Automation || x.Role == UserRole.DiAutomation)))
            {
                if (txtPassWord.Text == DanteCrypto.SifreBoz(_eRep.FirstOrDefault(x => x.UserName == txtUserName.Text).PassWord))
                {
                    Form3 form3 = new Form3(_eRep.FirstOrDefault(x => x.UserName == txtUserName.Text).ID);
                    form3.ShowDialog();
                    return;
                }
                MessageBox.Show("sifre Yanlis");
                return;

            }
            MessageBox.Show("You dont have access");
        }
    }
}
