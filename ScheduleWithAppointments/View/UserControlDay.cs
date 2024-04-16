using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ScheduleWithAppointments.DTO;
using ScheduleWithAppointments.BILL;
using System.Windows.Forms.Design;

namespace ScheduleWithAppointments.View
{
    public partial class UserControlDay : UserControl
    {
        Date date;
        int userID;
        public UserControlDay(dynamic date, int userID)
        {
            InitializeComponent();
            this.date = date;
            this.userID = userID;
        }

        private void UserControlDay_Load(object sender, EventArgs e)
        {
            this.txt.Text = date.day.ToString();

            getAppointmentEvent(this.userID);
        }

        private void getAppointmentEvent(int userID)
        {
            DateTime dtnow = DateTime.Now;

            DateTime formTime = Convert.ToDateTime($"00:00 {date.year}/{date.month}/{date.day}");

            //MessageBox.Show(startTime.ToString() +" "+ endTime.ToString());

            var ApppointmentEvents = new ScheduleAppointment_BILL().getAppointmentsByUserID(userID);

            int count = 0;

            foreach (var i in ApppointmentEvents) {

                //if (startTime <= i.startTime && i.startTime < endTime) // [startTime, endTime] \
                /*
                    overlap time:
                    + i.startTime < startTime
                 */

                DateTime startTime = Convert.ToDateTime($"00:00 {i.startTime.Year}/{i.startTime.Month}/{i.startTime.Day}");
                DateTime endTime = Convert.ToDateTime($"00:00 {i.endTime.Year}/{i.endTime.Month}/{i.endTime.Day}");
                if (startTime <= formTime && formTime <= endTime)
                {
                    if (count == 0)
                    {
                        UserControlAppointment appointmentEvent = new UserControlAppointment();
                        appointmentEvent.BackColor = Color.Green;
                        appointmentEvent.ForeColor = Color.White;
                        appointmentEvent.setText(i.AppointmentTitle);
                        eventContainer.Controls.Add(appointmentEvent);
                    }
                    count++;
                }

                //if (dtnow.Day == date.day && dtnow.Month == date.month && dtnow.Year == date.year)
                //{
                //    UserControlAppointment appointmentEvent = new UserControlAppointment();
                //    appointmentEvent.BackColor = Color.Green;
                //    appointmentEvent.ForeColor = Color.White;
                //    eventContainer.Controls.Add(appointmentEvent);

                //    UserControlAppointment appointmentEvent2 = new UserControlAppointment();
                //    appointmentEvent2.BackColor = Color.OrangeRed;
                //    appointmentEvent2.ForeColor = Color.White;
                //    appointmentEvent2.setText("+ 4 lịch trình...");
                //    eventContainer.Controls.Add(appointmentEvent2);
                //}
            }

            if (count > 1) {
                UserControlAppointment appointmentEvent = new UserControlAppointment();
                appointmentEvent.BackColor = Color.OrangeRed;
                appointmentEvent.ForeColor = Color.White;
                appointmentEvent.setText($"+ {count - 1} lịch trình...");
                eventContainer.Controls.Add(appointmentEvent);
            }


        }

    }
}
