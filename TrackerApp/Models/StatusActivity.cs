using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TrackerApp.Models
{
    public class StatusActivity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15)] [Required]
        public string StatusName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan Duration => (EndDate ?? DateTime.Now) - StartDate;

        public int DailySummaryId { get; set; } //if we write EntityNameId EF core automatically picks it up as a foreign key
        public DailySummary DailySummary { get; set; }

    }
}
