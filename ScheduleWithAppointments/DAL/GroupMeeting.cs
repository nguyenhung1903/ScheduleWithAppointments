using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWithAppointments
{
    public class GroupMeeting
    {
        public int Id { get; set; }
        public int GroupMeetingID { get; set; }
        public int UserID { get; set; }
    }
}
