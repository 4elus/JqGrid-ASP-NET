using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebAccount.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<AcademicPerformance> AcademicPerformances { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}