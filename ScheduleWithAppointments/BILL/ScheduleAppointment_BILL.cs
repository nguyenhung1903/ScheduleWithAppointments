using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScheduleWithAppointments.DTO;

namespace ScheduleWithAppointments.BILL
{
    class ScheduleAppointment_BILL
    {

        public List<CBBItem> getAllUsers()
        {
            List<CBBItem> ListUsers = new List<CBBItem>();
            using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
            {
                var data = from _user in db.Users select new {_user.UserID, _user.UserName};

                foreach (var item in data)
                {
                    ListUsers.Add(new CBBItem { Value = item.UserID, Text = item.UserName });
                }
                return ListUsers;
            }
        }

        public List<int> getUsersbyGroupMeetingID(int groupMeetingID)
        {
            List<int> UserIds = new List<int>(); 
            using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
            {
                var data = db.GroupMeetings.Where(prop => prop.GroupMeetingID == groupMeetingID).Select(p => new { p.UserID });

                foreach (var item in data)
                {
                    UserIds.Add(item.UserID);
                }
                return UserIds;
            }
        }

        public void AddUserToGroup(int userID, int groupID)
        {
            using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
            {
                db.GroupMeetings.Add(new GroupMeeting { GroupMeetingID = groupID, UserID = userID });
                db.SaveChanges();
            }
        }

        public Appointment checkAppointmentConflict(AppointmentItem ap, int userID)
        {
            var data = getAppointmentsByUserID(userID);

            foreach (var item in data)
            {
                bool overlap = (item.startTime <= ap.start_time && item.endTime >= ap.end_time)
                            || (item.startTime >= ap.start_time && item.startTime < ap.end_time)
                            || (item.endTime > ap.start_time && item.endTime <= ap.end_time);

                if (overlap) return item;
            }

            return null;
        }

        public int CheckAppointmentExistInGroupMeeting(AppointmentItem ap, int userID)
        {
            var data = getAllAppointments();

            foreach (var item in data)
            {
                bool overlap = (item.startTime <= ap.start_time && item.endTime >= ap.end_time)
                            || (item.startTime >= ap.start_time && item.startTime < ap.end_time)
                            || (item.endTime > ap.start_time && item.endTime <= ap.end_time);
                if (overlap && item.AppointmentTitle.Equals(ap.title))
                {
                    List<int> userIds = getUsersbyGroupMeetingID(item.GroupMeetingID);
                    foreach (var u in userIds)
                    {
                        if (userID == u) return -1;
                    }
                    return item.GroupMeetingID;
                }
            }
            
            return -1;
        }

        public bool AddAppointment(AppointmentItem p, int userID)
        {
            try
            {
                //throw new Exception(p.ToString());
                using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
                {

                    int gm = db.GroupMeetings.OrderBy(prop => prop.GroupMeetingID).Select(prop => prop.GroupMeetingID).LastOrDefault();

                    db.GroupMeetings.Add(new GroupMeeting { GroupMeetingID = gm + 1, UserID = userID });

                    Appointment appointment = new Appointment
                    {
                        AppointmentTitle = p.title,
                        Location = p.location,
                        startTime = p.start_time,
                        endTime = p.end_time,
                        UserID = userID,
                        AppointmentID = 1,
                        GroupMeetingID = gm + 1,
                    };

                    db.Appointments.Add(appointment);
                    if (p.isReminder == true) 
                        db.Reminders.Add(new Reminder { AppointmentID = 1, ReminderStatus = p.isReminder, ReminderTime = p.ReminderTime});
                    db.SaveChanges();
                }
                return true;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
           
        }

        public bool UpdateAppointment(int appointmentID, AppointmentItem p)
        {
            try
            {
                using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
                {

                    Appointment appointment = db.Appointments.Where(prop => prop.AppointmentID == appointmentID).FirstOrDefault();

                    appointment.AppointmentTitle = p.title;
                    appointment.startTime = p.start_time;
                    appointment.endTime = p.end_time;
                    appointment.Location = p.location;
                    
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }

        }

        public List<Appointment> getAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
            {
                var data = from appointment in db.Appointments
                           select new { appointment.AppointmentID, appointment.AppointmentTitle, appointment.Location, appointment.startTime, appointment.endTime, appointment.GroupMeetingID};

                foreach (var i in data)
                {
                    appointments.Add(new Appointment
                    {
                        AppointmentID = i.AppointmentID,
                        AppointmentTitle = i.AppointmentTitle,
                        Location = i.Location,
                        startTime = i.startTime,
                        endTime = i.endTime,
                        GroupMeetingID = i.GroupMeetingID
                    });
                }
            }

            return appointments;
        }

        public List<int> getUserIDsByGroupMeeting(int groupMeetingID)
        {
            List<int> userIds = new List<int>();
            using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
            {
                var data = from groupMeeting in db.GroupMeetings
                           where groupMeeting.GroupMeetingID == groupMeetingID
                           select new { groupMeeting.UserID };

                foreach (var i in data.ToList())
                {
                     userIds.Add(i.UserID);
                }
            }

            return userIds;
        }

        public List<Appointment> getAppointmentsByUserID(int userID)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (ScheduleAppointmentsDB db = new ScheduleAppointmentsDB())
            {
                var data = from _GroupMeeting in db.GroupMeetings
                           join appointment in db.Appointments on _GroupMeeting.GroupMeetingID equals appointment.GroupMeetingID
                           where _GroupMeeting.UserID == userID
                           select new { appointment.AppointmentID, appointment.AppointmentTitle, appointment.Location, appointment.startTime, appointment.endTime, _GroupMeeting.UserID};

                foreach (var i in data)
                {
                    appointments.Add(new Appointment
                    {
                        AppointmentID = i.AppointmentID,
                        AppointmentTitle = i.AppointmentTitle,
                        Location = i.Location,
                        startTime = i.startTime,
                        endTime = i.endTime,
                    });
                }
            }

            return appointments;
        }

    }
}
