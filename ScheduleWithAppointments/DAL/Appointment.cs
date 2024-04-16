using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWithAppointments
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public string AppointmentTitle { get; set; }
        public string Location { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int UserID { get; set; }

        public int GroupMeetingID { get; set; }

        /**
            User -> Appointments -> Reminders (true/false option)
            Group meeting ? => Users join Group => list of participants.

            // exceptions:
            *****
            * AppointmentTitle: don't empty
            * Duration time
            -----------------------------------------------------
            If same name => ask user "Duration appointment" => join group meeting
         */

        // UserCreateAppointment
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
