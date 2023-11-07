using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WINUI
{
    public partial class Salon2 : Form
    {
        Movie _m;
        ReservationRepository _rRep;
        SalonRepository _sRep;
        TicketRepository _tRep;
        MovieRepository _mRep;
        int _employeeID;
        int _salonNo;
        public Salon2(int employeeID, string movieName, int salonNo)
        {
            InitializeComponent();
            _employeeID = employeeID;
            Text = movieName;
            _salonNo = salonNo;
            _sRep = new SalonRepository();
            _rRep = new ReservationRepository();
            _tRep = new TicketRepository();
            _mRep = new MovieRepository();
            _m = _mRep.FirstOrDefault(x => x.FilmTitle == movieName);
            cmbSeans.DataSource = _m.Seanses;

        }
        /// <summary>
        /// Ekranda Çıkan biletlere tıklayarak onları kırmızıya boyar ve ticket tablosuna ekler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eId"></param>
        /// <param name="session"></param>
        /// <param name="picker"></param>
        /// <param name="salonNo"></param>
        /// <param name="filmName"></param>
        /// <param name="_tRep"></param>
        private void TicketSelect(object sender, int eId, ComboBox session, DateTimePicker picker, TicketRepository _tRep)
        {
            if (picker.Value.Date >= DateTime.Now.Date && picker.Value.Date <= DateTime.Now.AddDays(2).Date)
            {
                if (session.Text == "11:00" && DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0) && picker.Value.Day == DateTime.Today.Day)
                {
                    MessageBox.Show("You can't sell ticket after the movie starts.");
                    return;
                }
                else if (session.Text == "14:00" && DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0) && picker.Value.Day == DateTime.Today.Day)
                {
                    MessageBox.Show("You can't sell ticket after the movie starts.");
                    return;
                }
                else if (session.Text == "17:00" && DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0) && picker.Value.Day == DateTime.Today.Day)
                {
                    MessageBox.Show("You can't sell ticket after the movie starts.");
                    return;
                }
                else if (session.Text == "20:00" && DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0) && picker.Value.Day == DateTime.Today.Day)
                {
                    MessageBox.Show("You can't sell ticket after the movie starts.");
                    return;
                }
                else
                {
                    Ticket ticket = new Ticket();
                    ticket.EmployeeID = eId;
                    ticket.Session = session.SelectedItem as Session;
                    ticket.SeatNumber = (sender as Label).Name;
                    ticket.TicketDate = picker.Value.Date;
                    ticket.Type = TicketType.Ticket;
                    (session.SelectedItem as Session).TotalRatio++;
                    _tRep.Add(ticket);
                    (sender as Label).BackColor = Color.Azure;
                    (sender as Label).Enabled = false;
                    return;
                }

            }
            MessageBox.Show("You can buy tickets at least 2 days in advance");
        }
        /// <summary>
        /// Oturma planını günlere göre Renklendirerek gösterir.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="pick"></param>
        /// <param name="grp"></param>
        /// <param name="_tRep"></param>
        private void TicketShow(ComboBox session, DateTimePicker pick, GroupBox grp, TicketRepository _tRep, ReservationRepository _rRep)
        {
            foreach (Ticket ticket in _tRep.GetNotPassives())
            {
                if (ticket.TicketDate.Date != pick.Value.Date || ticket.SessionID != (session.SelectedItem as Session).ID)
                {
                    foreach (Label item in grp.Controls)
                    {
                        if (ticket.SeatNumber != item.Name && ticket.Active == false)
                        {
                            item.BackColor = Color.Green;
                            item.Enabled = true;
                        }
                    }
                }
            }
            foreach (Reservation rez in _rRep.GetInserteds())
            {
                if (rez.TicketTime.Date != pick.Value.Date || rez.SessionID != (session.SelectedItem as Session).ID)
                {
                    foreach (Label item in grp.Controls)
                    {
                        if (rez.ResSeatNo == item.Name)
                        {

                            item.BackColor = Color.Green;
                            item.Enabled = true;
                        }
                    }
                }
            }
            foreach (Ticket ticket in _tRep.GetNotPassives())
            {
                if (ticket.TicketDate.Date == pick.Value.Date && ticket.SessionID == (session.SelectedItem as Session).ID)
                {
                    foreach (Label item in grp.Controls)
                    {
                        if (ticket.SeatNumber == item.Name && ticket.Type == TicketType.Ticket && ticket.Active == true)
                        {
                            item.BackColor = Color.Red;
                            item.Enabled = false;
                        }
                    }
                }
            }
            foreach (Reservation rez in _rRep.GetAll())
            {
                if (rez.TicketTime.Date == pick.Value.Date && rez.SessionID == (session.SelectedItem as Session).ID)
                {
                    foreach (Label item in grp.Controls)
                    {
                        if (rez.ResSeatNo == item.Name && rez.Type == TicketType.Rez)
                        {
                            item.BackColor = Color.Yellow;
                            item.Enabled = false;
                        }
                        else if (rez.ResSeatNo == item.Name && rez.Type == TicketType.Ticket)
                        {
                            item.BackColor = Color.Red;
                            item.Enabled = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Oldugumuz gün Oncesindeki biletleri kitler
        /// </summary>
        /// <param name="pick"></param>
        /// <param name="grp"></param>
        /// <param name="_tRep"></param>
        private void SalonCloser(DateTimePicker pick, GroupBox grp, TicketRepository _tRep)
        {
            foreach (Ticket ticket in _tRep.GetAll())
            {
                if (pick.Value.Date < DateTime.Now.Date)
                {
                    foreach (Label item in grp.Controls)
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        private void lblA2_Click(object sender, EventArgs e)
        {
           TicketSelect(sender, _employeeID, cmbSeans,dtpTicket,_tRep);
        }
        private void dtpTicket_ValueChanged(object sender, EventArgs e)
        {
           TicketShow(cmbSeans, dtpTicket, grp1, _tRep,_rRep);
           SalonCloser(dtpTicket, grp1, _tRep);
        }
        private void cmbSeans_SelectedIndexChanged(object sender, EventArgs e)
        {
           TicketShow(cmbSeans, dtpTicket, grp1, _tRep, _rRep);
           SalonCloser(dtpTicket, grp1, _tRep);
        } 
    }
}
