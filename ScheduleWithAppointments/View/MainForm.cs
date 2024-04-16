using ScheduleWithAppointments.DTO;
using ScheduleWithAppointments.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScheduleWithAppointments.BILL;

namespace ScheduleWithAppointments
{
    public partial class MainForm : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }


        int month, year;
        private bool mouseDown;
        private Point lastLocation;

        Date currDay = new Date();

        public MainForm()
        {
            InitializeComponent();
        }

        private Date beforeMonth(int m, int y)
        {
            int _month = m;
            int _year = y;

            _month--;
            if (_month < 1)
            {
                _month = 12;
                _year -= 1;
            }

            return new Date { month = _month, year = _year };
        }

        private int getDayInMonth(Date date)
        {
            return DateTime.DaysInMonth(date.year, date.month);
        }

        private void displayDays(int _month, int _year)
        {

            int userID = ((CBBItem)(cbbUser.SelectedItem)).Value;

            lbMonthYear.Text = $"{DateTimeFormatInfo.CurrentInfo.GetMonthName(_month)} / {_year}";

            dayContainer.Controls.Clear();

            DateTime startOftheMonth = new DateTime(_year, _month, 1);
            int days = DateTime.DaysInMonth(_year, _month);

            int dayOftheWeek = Convert.ToInt32(startOftheMonth.DayOfWeek.ToString("d"));

            Date _beforeMonth =  beforeMonth(_month, _year);
            int beforeDays = getDayInMonth(_beforeMonth);

            for (int i = dayOftheWeek; i >= 1; i--)
            {
                UserControlDay d = new UserControlDay(new Date { day = beforeDays - i + 1, month = _beforeMonth.month, year = _beforeMonth.year }, userID);
                d.BackColor = Color.LightGray;
                dayContainer.Controls.Add(d);
            }

            for (int i = 1; i <= days; i++)
            {
                UserControlDay d = new UserControlDay(new Date { day = i, month = _month, year = _year}, userID);

                if (i == currDay.day && _month == currDay.month && _year == currDay.year)
                {
                    d.BackColor = Color.LightPink;
                }

                else d.BackColor = Color.White;
                dayContainer.Controls.Add(d);
            }

            for (int i = 1; i <= 42 - dayOftheWeek - days; ++i)
            {
                UserControlDay d = new UserControlDay(new Date { day = i, month = 12, year = 9999 }, userID);
                d.BackColor = Color.LightGray;
                dayContainer.Controls.Add(d);
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            month++;
            if (month > 12)
            {
                month = 1;
                year += 1;
            }
            displayDays(month, year);
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            month--;
            if (month < 1)
            {
                month = 12;
                year -= 1;
            }
            displayDays(month, year);
        }

        private void btnHidden_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int groupID = ((CBBItem)(cbbUser.SelectedItem)).Value;
            AppointmentForm appointmentForm = new AppointmentForm(groupID);
            appointmentForm.UpdateForm = new AppointmentForm.ScheduleWithAppointmentsListView_SelectionChanged(UpdateForm);
            appointmentForm.Show();
        }

        private void UpdateForm()
        {
            cbbUser.Items.Clear();
            cbbUser.Items.AddRange(new ScheduleAppointment_BILL().getAllUsers().ToArray());
            cbbUser.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void CBBUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtnow = DateTime.Now;
            month = dtnow.Month;
            year = dtnow.Year;

            currDay.day = dtnow.Day;
            currDay.month = month;
            currDay.year = year;
            this.displayDays(month, year);
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }


    }
}
