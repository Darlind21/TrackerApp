using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerApp.Models;

namespace TrackerApp.Services
{
    public class TrackerAppService : IDisposable
    {
        private readonly TrackerAppDbContext _context;
        public TrackerAppService()
        {
            _context = new TrackerAppDbContext();
        }

        private bool _disposed = false;
        public void Dispose()
        {
            // Call the protected Dispose method with true 
            Dispose(true);
            // Tell Garbage Collector not to finalize this object
            //In plain english tells the GC that it is already disposed so no need to do it again 
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine
                (
                "\n" +
                $"You are on {workingActivity.StatusName} status." +
                $"\nYou have started this status on: {workingActivity.StartDate.ToShortTimeString()}." +
                $"\nDuration until now for this status: {workingActivity.Duration.TotalMinutes.ToString("0.0")} minutes or {workingActivity.Duration.TotalHours.ToString("0.0")} hours." +
                $"\nYou have spent a total of {workingSummary.ToString("0.0")} minutes on this status for today"
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

            var breakSummary = (breakActivity.DailySummary.BreakDuration ?? 0) + (int)breakActivity.Duration.TotalMinutes;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine
                (
                "\n" +
                $"You are on {breakActivity.StatusName} status." +
                $"\nYou have started this status on: {breakActivity.StartDate.ToShortTimeString()}." +
                $"\nDuration until now for this status: {breakActivity.Duration.TotalMinutes.ToString("0.0")} minutes or {breakActivity.Duration.TotalHours.ToString("0.0")} hours." +
                $"\nYou have spent a total of {breakSummary.ToString("0.0")} minutes on this status for today"
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine
                (
                "\n" +
                $"You are on {awayActivity.StatusName} status." +
                $"\nYou have started this status on: {awayActivity.StartDate.ToShortTimeString()}." +
                $"\nDuration until now for this status: {awayActivity.Duration.TotalMinutes.ToString("0.0")} minutes or {awayActivity.Duration.TotalHours.ToString("0.0")} hours." +
                $"\nYou have spent a total of {awaySummary.ToString("0.0")} minutes on this status for today"
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
                Console.WriteLine("Daily summary not found or you have not started tracking for today yet");
                return;
            }

            var todaysStatusActivities = todaysDailySummary.StatusActivities.OrderBy(sa => sa.StartDate).ToList();

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("\n");
            foreach (var tsa in todaysStatusActivities)
            {
                Console.WriteLine($"Status: {tsa.StatusName}, \nStart Time: {tsa.StartDate.ToShortTimeString()}, " +
                    $"\nEnd Time: {tsa.EndDate?.ToShortTimeString() ?? "Has not ended"}" +
                    $"\nDuration: {tsa.Duration.TotalMinutes.ToString("0.0")} minutes or {tsa.Duration.TotalHours.ToString("0.0")} hours." +
                    $"\n");
            }

            //we are creating variables for the status the user is on right now so we can add it to the total time spent in that status when displaying daily summary
            var latestWorkingActivity = todaysDailySummary.StatusActivities
                .Where(sa => sa.StatusName == "Working")
                .OrderByDescending(sa => sa.StartDate)
                .FirstOrDefault();

            int latestWorkingActivityDuration;
            if (latestWorkingActivity != null)
            {
                latestWorkingActivityDuration = (int) latestWorkingActivity.Duration.TotalMinutes;
            }
            else
            {
                latestWorkingActivityDuration = 0;
            }


            var latestBreakActivity = todaysDailySummary.StatusActivities
                .Where(sa => sa.StatusName == "Break")
                .OrderByDescending(sa => sa.StartDate)
                .FirstOrDefault();

            int latestBreakActivityDuration;
            if (latestBreakActivity != null)
            {
                latestBreakActivityDuration = (int)latestBreakActivity.Duration.TotalMinutes;
            }
            else
            {
                latestBreakActivityDuration = 0;
            }


            var latestAwayActivity = todaysDailySummary.StatusActivities
                .Where(sa => sa.StatusName == "Away")
                .OrderByDescending(sa => sa.StartDate)
                .FirstOrDefault();

            int latestAwayActivityDuration;
            if (latestAwayActivity != null)
            {
                latestAwayActivityDuration = (int)latestAwayActivity.Duration.TotalMinutes;
            }
            else
            {
                latestAwayActivityDuration = 0;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{todaysDailySummary.StatusDate.ToString("d")} " +
                $"\n=> Total time spent on WORKING: {todaysDailySummary.WorkingDuration + latestWorkingActivityDuration} minutes." +
                $"\n=> Total time spent on BREAK: {todaysDailySummary.BreakDuration + latestBreakActivityDuration} minutes." +
                $"\n=> Total time spent on AWAY: {todaysDailySummary.AwayDuration + latestAwayActivityDuration} minutes." +
                $"\n");
        }

        public void DisplayLastSevenDaysSummary()
        {
            var lastSevenDaysSummary = _context.DailySummaries
                .OrderByDescending(ds => ds.StatusDate)
                .Take(7)
                .OrderBy(ds => ds.StatusDate)
                .ToList();

            if (lastSevenDaysSummary.Count == 0)
            {
                Console.WriteLine("You do not have any daily summaries yet");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Showing daily summaries for the past {lastSevenDaysSummary.Count()} days. \n");

            foreach (var ds in lastSevenDaysSummary)
            {
                Console.WriteLine($"{ds.StatusDate.ToString("d")} " +
                $"\n=> Total time spent on WORKING: {ds.WorkingDuration ?? 0} minutes." +
                $"\n=> Total time spent on BREAK: {ds.BreakDuration ?? 0 } minutes." +
                $"\n=> Total time spent on AWAY: {ds.AwayDuration ?? 0} minutes." +
                $"\n");
            }
        }

        public void CleanUpBeforeExit()
        {
            var unendedStatusActivity = _context.StatusActivities
                .OrderByDescending(sa => sa.StartDate)
                .Where(sa => sa.EndDate == null)
                .FirstOrDefault();

            if (unendedStatusActivity == null)
            {
                return;
            }

            if (unendedStatusActivity.StatusName == "Working")
            {
                EndWorkingStatusActivity(unendedStatusActivity.Id);
            }
            else if (unendedStatusActivity.StatusName == "Break")
            {
                EndBreakStatusActivity(unendedStatusActivity.Id);
            }
            else if(unendedStatusActivity.StatusName == "Away")
            {
                EndAwayStatusActivity(unendedStatusActivity.Id);
            }
        }
    }
}
