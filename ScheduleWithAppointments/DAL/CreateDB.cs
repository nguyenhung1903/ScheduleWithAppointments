using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWithAppointments.DAL
{
    public class CreateDB: DropCreateDatabaseAlways<ScheduleAppointmentsDB>
    {
        protected override void Seed(ScheduleAppointmentsDB context)
        {
            context.Users.Add(new User { UserID = 1, UserName = "test01"});
            context.Users.Add(new User { UserID = 2, UserName = "test02" });
            context.Users.Add(new User { UserID = 3, UserName = "test03" });

            context.GroupMeetings.AddRange(new GroupMeeting[]
            {
                new GroupMeeting {GroupMeetingID = 1, UserID =  1},
                new GroupMeeting {GroupMeetingID = 1, UserID =  2}
            });

            context.Appointments.AddRange(new Appointment[] {
                new Appointment { 
                    AppointmentID = 1,
                    AppointmentTitle = "Hello world, ScheduleApp is coming...",
                    startTime = Convert.ToDateTime("5:20 2023/04/06"),
                    endTime = Convert.ToDateTime("21:30 2023/04/07"),
                    Location = "DUT",
                    GroupMeetingID = 1,
                    UserID = 1
                }
            });

            context.Reminders.AddRange(new Reminder[] {
                new Reminder {AppointmentID = 1, ReminderStatus = true, ReminderTime = 5}
            });

            /*context.Groups.AddRange(new Group[] {
                new Group {GroupID = 1, GroupName = "21T_DT"},
                new Group {GroupID = 2, GroupName = "21T_DT2"},
            });

            

            context.Appointments.Add(new Appointment
            {
                AppointmentID = 1,
                AppointmentTitle = "Hello world, ScheduleApp is coming...",
                startTime = Convert.ToDateTime("5:20 2023/04/06"),
                endTime = Convert.ToDateTime("21:30 2023/04/07"),
                Location = "DUT",
                GroupID = 1,
            });

            context.Appointments.Add(new Appointment
            {
                AppointmentID = 2,
                AppointmentTitle = "Balala...",
                startTime = Convert.ToDateTime("5:20 2023/04/06"),
                endTime = Convert.ToDateTime("21:30 2023/04/06"),
                Location = "DUT",
                GroupID = 1,
            });

            context.Reminders.Add(new Reminder
            {
                AppointmentID = 1,
                ReminderId = 1,
                ReminderStatus = true,
            });*/

        }
    }
}
