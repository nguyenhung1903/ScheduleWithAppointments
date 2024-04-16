using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWithAppointments
{
    public class User
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        public string UserName { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public User() { 
            Appointments = new HashSet<Appointment>();
        }

/*        public int GroupID { get; set; }

        [ForeignKey("GroupID")]
        public virtual GroupMeeting GroupMeeting { get; set; } // Default GroupID = 0 => Use don't join any group meeting...*/
    }
}
