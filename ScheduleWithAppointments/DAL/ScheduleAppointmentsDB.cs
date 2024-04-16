using System;
using System.Data.Entity;
using System.Linq;
using ScheduleWithAppointments.DAL;  

namespace ScheduleWithAppointments
{
    public class ScheduleAppointmentsDB : DbContext
    {
        // Your context has been configured to use a 'ScheduleAppointmentsDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ScheduleWithAppointments.ScheduleAppointmentsDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ScheduleAppointmentsDB' 
        // connection string in the application configuration file.
        public ScheduleAppointmentsDB()
            : base("name=ScheduleAppointmentsDB")
        {
            Database.SetInitializer<ScheduleAppointmentsDB>(new CreateDB());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<GroupMeeting> GroupMeetings { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}