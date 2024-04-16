using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWithAppointments.DTO
{
    class AppointmentItem
    {
        public string title { get; set; }
        public string location { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public bool isReminder { get; set; }
        public int ReminderTime { get; set; }

    }
}
