using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerApp.Models;

namespace TrackerApp.Services
{
    public class TrackerAppService
    {
        private readonly TrackerAppDbContext _context;
        public TrackerAppService()
        {
            _context = new TrackerAppDbContext();
        }


        public int CreateWorkingStatusActivity() //returns Id of created activity in order to make it easier to find Working activity to be 
                                                 //ended by EndWorkingStatusActivity().
        {
            var WorkingStatusActivity = new StatusActivity
            {
                StatusName = "Working",
                StartDate = DateTime.Now,
            };
            _context.StatusActivities
                .Add(WorkingStatusActivity);

            DailySummary? result = _context.DailySummaries
                .Where(ds => ds.StatusDate == DateOnly.FromDateTime(DateTime.Now))
                .SingleOrDefault();

            if (result == null)
            {
                var todaysDailySummary = new DailySummary
                {
                    StatusDate = DateOnly.FromDateTime(DateTime.Now)
                };

                todaysDailySummary.StatusActivities.Add(WorkingStatusActivity);
                WorkingStatusActivity.DailySummaryId = todaysDailySummary.Id;
                WorkingStatusActivity.DailySummary = todaysDailySummary;

                _context.DailySummaries
                    .Add(todaysDailySummary);
            }
            else if (result != null)
            {
                result.StatusActivities.Add(WorkingStatusActivity);
                WorkingStatusActivity.DailySummaryId = result.Id;
                WorkingStatusActivity.DailySummary = result;
            }

            _context.SaveChanges();

            return WorkingStatusActivity.Id;
        }

        public void EndWorkingStatusActivity(int WorkingActivityId)
        {
            StatusActivity? ToBeChangedActivity = _context.StatusActivities
                .Find(WorkingActivityId);

            if (ToBeChangedActivity == null)
            {
                Console.WriteLine($"Status activity with id: {WorkingActivityId} does not exist");
                return;
            }

            if (ToBeChangedActivity.EndDate != null)
            {
                Console.WriteLine($"Activity: {ToBeChangedActivity.StatusName} has ended.");
            }
            else
            {
                ToBeChangedActivity.EndDate = DateTime.Now;
                ToBeChangedActivity.DailySummary.WorkingDuration += ((int)ToBeChangedActivity.Duration.TotalMinutes);
            }

        }

        public int CreateBreakStatusActivity()
        {
            var BreakStatusActivity = new StatusActivity
            {
                StatusName = "Break",
                StartDate = DateTime.Now,
            };
            _context.StatusActivities
                .Add(BreakStatusActivity);

            DailySummary? result = _context.DailySummaries
                .Where(ds => ds.StatusDate == DateOnly.FromDateTime(DateTime.Now))
                .SingleOrDefault();

            if (result == null)
            {
                var todaysDailySummary = new DailySummary
                {
                    StatusDate = DateOnly.FromDateTime(DateTime.Now)
                };

                todaysDailySummary.StatusActivities.Add(BreakStatusActivity);
                BreakStatusActivity.DailySummaryId = todaysDailySummary.Id;
                BreakStatusActivity.DailySummary = todaysDailySummary;

                _context.DailySummaries
                    .Add(todaysDailySummary);
            }
            else if (result != null)
            {
                result.StatusActivities.Add(BreakStatusActivity);
                BreakStatusActivity.DailySummaryId = result.Id;
                BreakStatusActivity.DailySummary = result;
            }

            _context.SaveChanges();

            return BreakStatusActivity.Id;
        }

        public void EndBreakStatusActivity(int BreakActivityId)
        {
            StatusActivity? ToBeChangedActivity = _context.StatusActivities
                .Find(BreakActivityId);

            if (ToBeChangedActivity == null)
            {
                Console.WriteLine($"Status activity with id: {BreakActivityId} does not exist");
                return;
            }

            if (ToBeChangedActivity.EndDate != null)
            {
                Console.WriteLine($"Activity: {ToBeChangedActivity.StatusName} has ended.");
            }
            else
            {
                ToBeChangedActivity.EndDate = DateTime.Now;
                ToBeChangedActivity.DailySummary.BreakDuration += ((int)ToBeChangedActivity.Duration.TotalMinutes);
            }

        }

        public int CreateAwayStatusActivity()
        {
            var AwayStatusActivity = new StatusActivity
            {
                StatusName = "Away",
                StartDate = DateTime.Now,
            };
            _context.StatusActivities
                .Add(AwayStatusActivity);

            DailySummary? result = _context.DailySummaries
                .Where(ds => ds.StatusDate == DateOnly.FromDateTime(DateTime.Now))
                .SingleOrDefault();

            if (result == null)
            {
                var todaysDailySummary = new DailySummary
                {
                    StatusDate = DateOnly.FromDateTime(DateTime.Now)
                };

                todaysDailySummary.StatusActivities.Add(AwayStatusActivity);
                AwayStatusActivity.DailySummaryId = todaysDailySummary.Id;
                AwayStatusActivity.DailySummary = todaysDailySummary;

                _context.DailySummaries
                    .Add(todaysDailySummary);
            }
            else if (result != null)
            {
                result.StatusActivities.Add(AwayStatusActivity);
                AwayStatusActivity.DailySummaryId = result.Id;
                AwayStatusActivity.DailySummary = result;
            }

            _context.SaveChanges();

            return AwayStatusActivity.Id;
        }

        public void EndAwayStatusActivity(int AwayActivityId)
        {
            StatusActivity? ToBeChangedActivity = _context.StatusActivities
                .Find(AwayActivityId);

            if (ToBeChangedActivity == null)
            {
                Console.WriteLine($"Status activity with id: {AwayActivityId} does not exist");
                return;
            }

            if (ToBeChangedActivity.EndDate != null)
            {
                Console.WriteLine($"Activity: {ToBeChangedActivity.StatusName} has ended.");
            }
            else
            {
                ToBeChangedActivity.EndDate = DateTime.Now;
                ToBeChangedActivity.DailySummary.WorkingDuration += ((int)ToBeChangedActivity.Duration.TotalMinutes);
            }

        }
    }
}
