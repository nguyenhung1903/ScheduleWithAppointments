using ScheduleWithAppointments.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScheduleWithAppointments.BILL;

namespace ScheduleWithAppointments.View
{
    public partial class AppointmentForm : Form
    {
        public delegate void ScheduleWithAppointmentsListView_SelectionChanged();
        public ScheduleWithAppointmentsListView_SelectionChanged UpdateForm { get; set; }

        private bool mouseDown;
        private Point lastLocation;

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

        int userID;
        public AppointmentForm(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }   

        private void AppointmentForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void AppointmentForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void AppointmentForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private AppointmentItem getParamsAndValidation()
        {
            string title = txtTitle.Text;
            string location = txtLocation.Text;
            DateTime start_time = dtpStart.getDateTime();
            DateTime end_time = dtpEnd.getDateTime();
            bool isReminder = cboxReminder.Checked;
            int ReminderTime = ((CBBItem)(cbbReminderTime.SelectedItem)).Value;

            // data validating...
            if (title == " " || title == "" || title == "\n") throw new Exception("Title can't emply");
            if (location == " " || location == "" || location == "\n") throw new Exception("Location can't emply");
            if (end_time <= start_time) throw new Exception("end time can't bigger than start time");
            
            return new AppointmentItem { title = title, location = location, start_time = start_time, end_time = end_time, isReminder = isReminder, ReminderTime = ReminderTime};
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                AppointmentItem p = getParamsAndValidation();

                ScheduleAppointment_BILL bill = new ScheduleAppointment_BILL();
                Appointment appointmentObject = bill.checkAppointmentConflict(p, userID);
                int groupID = bill.CheckAppointmentExistInGroupMeeting(p, userID);
                if (appointmentObject != null)
                {
                    string message = "You already have an appointment at that time! Please choose another avalible time or replace. Do you want to replace appointment?";
                    string caption = "Warning!";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    // Displays the MessageBox.
                    result = MessageBox.Show(this, message, caption, buttons);
                    if (result == DialogResult.Yes)
                    {
                        bill.UpdateAppointment(appointmentObject.AppointmentID, p);
                        this.Close();
                    }
                } if (groupID != -1)
                {
                    string message = "Your appointment same name and time in another group meeting... Do you want to join that group meeting?";
                    string caption = "Warning!";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    // Displays the MessageBox.
                    result = MessageBox.Show(this, message, caption, buttons);
                    if (result == DialogResult.Yes)
                    {
                        bill.AddUserToGroup(userID, groupID);
                        this.Close();
                    }
                } if (groupID == 0)
                {
                    // don't have event
                }
                else
                {
                    if (bill.AddAppointment(p, userID))
                    {
                        MessageBox.Show("Add appointment is successful");
                    }
                    else
                    {
                        MessageBox.Show("Add appointment is error!");
                    }
                }

                UpdateForm();
                this.Dispose();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            this.cbbReminderTime.Items.AddRange(new CBBItem[]
            {
                new CBBItem {Value = 5, Text = "5 Minutes"},
                new CBBItem {Value = 10, Text = "10 Minutes"},
                new CBBItem {Value = 20, Text = "20 Minutes"},
                new CBBItem {Value = 30, Text = "30 Minutes"},
                new CBBItem {Value = 60, Text = "1 Hour"},
                new CBBItem {Value = 120, Text = "2 Hours"},
            });
            this.cbbReminderTime.SelectedIndex = 0;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            String caption = $@"Mỗi participant được phân tách nhau bởi kí tự ','. Ví dụ: test01,test02";
            MessageBox.Show(caption, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
