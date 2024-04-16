using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWithAppointments
{
    public class Reminder
    {
        [Key]
        public int ReminderId { get; set; }
        public bool ReminderStatus { get; set;}
        public int ReminderTime { get; set; }
        public int AppointmentID { get; set; }

        [ForeignKey("AppointmentID")]
        public virtual Appointment Appointment { get; set; }
    }
}
