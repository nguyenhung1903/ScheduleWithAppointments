using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleWithAppointments.View
{
    public partial class UserControlDatetime : UserControl
    {
        public UserControlDatetime()
        {
            InitializeComponent();
        }


        public DateTime getDateTime()
        {
            string _t = dtpTime.Value.ToString("HH:mm");
            string _d = dtpDate.Value.ToString("d");
            string dt = $"{_t} {_d}";
           // throw new Exception(dt);

            return Convert.ToDateTime(dt);
        }
    }
}
