using Microsoft.EntityFrameworkCore;
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
                .Where(sa => sa.Id == WorkingActivityId)
                .SingleOrDefault();


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
                var dailySummaryForWorkingStatusActicity = _context.DailySummaries
                    .Where(ds => ds.Id == ToBeChangedActivity.DailySummaryId)
                    .SingleOrDefault();

                dailySummaryForWorkingStatusActicity.WorkingDuration = (dailySummaryForWorkingStatusActicity.WorkingDuration ?? 0) + (int)Math.Round(ToBeChangedActivity.Duration.TotalMinutes);
                _context.SaveChanges();
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
                .Where(sa => sa.Id == BreakActivityId)
                .SingleOrDefault();

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
                var dailySummaryForBreakStatusActivity = _context.DailySummaries
                    .Where(ds => ds.Id == ToBeChangedActivity.DailySummaryId)
                    .SingleOrDefault();

                dailySummaryForBreakStatusActivity.BreakDuration = (dailySummaryForBreakStatusActivity.BreakDuration ?? 0) + (int)Math.Round(ToBeChangedActivity.Duration.TotalMinutes);
                _context.SaveChanges();
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
                .Where(sa => sa.Id == AwayActivityId)
                .SingleOrDefault();

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
                var dailySummaryForAwayStatusActivity = _context.DailySummaries
                    .Where(ds => ds.Id == ToBeChangedActivity.DailySummaryId)
                    .SingleOrDefault();

                dailySummaryForAwayStatusActivity.AwayDuration = (dailySummaryForAwayStatusActivity.AwayDuration ?? 0) + (int)Math.Round(ToBeChangedActivity.Duration.TotalMinutes);
                _context.SaveChanges();
            }

        }

        public void DisplayWorkingStatusActivityDetails( int workingStatusId)
        {
            var workingActivity = _context.StatusActivities
                .Where(sa => sa.Id == workingStatusId)
                .Include(sa => sa.DailySummary)
                .SingleOrDefault();
                
            if (workingActivity == null)
            {
                Console.WriteLine("Working activity not found ");
                return;
            }

            var workingSummary = (workingActivity.DailySummary.WorkingDuration ?? 0) + (int)workingActivity.Duration.TotalMinutes;

            Console.WriteLine
                (
                "\n" +
                $"You are on {workingActivity.StatusName} status." +
                $"\nYou have started this status on: {workingActivity.StartDate.ToShortTimeString()}." +
                $"\nDuration until now for this status: {workingActivity.Duration.TotalMinutes} minutes or {workingActivity.Duration.TotalHours} hours." +
                $"\nYou have spent a total of {workingSummary} minutes on this status for today"
                );
        }

        public void DisplayBreakStatusActivityDetails(int breakStatusId)
        {
            var breakActivity = _context.StatusActivities
                .Where(sa => sa.Id == breakStatusId)
                .Include(sa => sa.DailySummary)
                .SingleOrDefault();

            if (breakActivity == null)
            {
                Console.WriteLine("Break activity not found ");
                return;
            }

            var breakSummary = (breakActivity.DailySummary.WorkingDuration ?? 0) + (int)breakActivity.Duration.TotalMinutes;

            Console.WriteLine
                (
                "\n" +
                $"You are on {breakActivity.StatusName} status." +
                $"\nYou have started this status on: {breakActivity.StartDate.ToShortTimeString()}." +
                $"\nDuration until now for this status: {breakActivity.Duration.TotalMinutes} minutes or {breakActivity.Duration.TotalHours} hours." +
                $"\nYou have spent a total of {breakSummary} minutes on this status for today"
                );
        }

        public void DisplayAwayStatusActivityDetails(int awayStatusId)
        {
            var awayActivity = _context.StatusActivities
                .Where(sa => sa.Id == awayStatusId)
                .Include(sa => sa.DailySummary)
                .SingleOrDefault();

            if (awayActivity == null)
            {
                Console.WriteLine("Away activity not found ");
                return;
            }

            var awaySummary = (awayActivity.DailySummary.AwayDuration ?? 0) + (int)awayActivity.Duration.TotalMinutes;

            Console.WriteLine
                (
                "\n" +
                $"You are on {awayActivity.StatusName} status." +
                $"\nYou have started this status on: {awayActivity.StartDate.ToShortTimeString()}." +
                $"\nDuration until now for this status: {awayActivity.Duration.TotalMinutes} minutes or {awayActivity.Duration.TotalHours} hours." +
                $"\nYou have spent a total of {awaySummary} minutes on this status for today"
                );
        }

        public void DisplayTodaysStatusActivitiesAndSummary()
        {
            DateOnly todaysDate = DateOnly.FromDateTime(DateTime.Now);

            var todaysDailySummary = _context.DailySummaries
                .Where(ds => ds.StatusDate == todaysDate)
                .Include(ds => ds.StatusActivities)
                .SingleOrDefault();

            if (todaysDailySummary == null)
            {
                Console.WriteLine("Daily summary not found");
                return;
            }

            var todaysStatusActivities = todaysDailySummary.StatusActivities.ToList();

            foreach (var tsa in todaysStatusActivities)
            {
                Console.WriteLine($"Status: {tsa.StatusName}, Start Time: {tsa.StartDate.ToShortTimeString()}, " +
                    $"End Time: {tsa.EndDate?.ToShortTimeString() ?? "Has not ended"}" +
                    $"\nDuration: {tsa.Duration.TotalMinutes} minutes or {tsa.Duration.TotalHours} hours." +
                    $"\n");
            }

            Console.WriteLine($"{todaysDailySummary.StatusDate.ToString("d")} -- Does not include current status duration " +
                $"\n=> Total time spent on WORKING: {todaysDailySummary.WorkingDuration} minutes." +
                $"\n=> Total time spent on BREAK: {todaysDailySummary.BreakDuration} minutes." +
                $"\n=> Total time spent on AWAY: {todaysDailySummary.AwayDuration} minutes." +
                $"\n");
        }
    }
}
