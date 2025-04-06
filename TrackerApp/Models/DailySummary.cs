using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace TrackerApp.Models
{
    public class DailySummary
    {
        [Key]
        public int Id { get; set; }

        public DateOnly StatusDate { get; set; }

        public int? WorkingDuration { get; set; } //Duration in minutes
        public int? BreakDuration { get; set; }
        public int? AwayDuration { get; set; }

        public List<StatusActivity> StatusActivities { get; set; } = new List<StatusActivity>();

    }
}
