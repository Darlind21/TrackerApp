using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrackerApp.Models;

namespace TrackerApp
{
    public class TrackerAppDbContext : DbContext
    {
        public DbSet<StatusActivity> StatusActivities { get; set; }
        public DbSet<DailySummary> DailySummaries { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O0OAK89\SQLEXPRESS;Database=TrackerApp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
    }
}
