using System;
using TrackerApp.Services;

namespace TrackerApp
{
    class Program
    {
        public static void Main()
        {

            using (var service = new TrackerAppService())
            {
                AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
                {
                    service.CleanUpBeforeExit();
                    service.Dispose();
                };

                StartMenu:
                int startResult = AppMenu.PrintStartMenu();
                switch (startResult)
                {
                    case 1: //After - Press 1 to start tracking
                        ChooseStatusMenu:
                        int chosenStatusResult = AppMenu.PrintChooseStatusMenu();
                        switch (chosenStatusResult) //choose from statuses menu
                        {
                            case 1: //Press 1 to start tracking - Press 1 to select WORKING
                                int workingStatusId = service.CreateWorkingStatusActivity();

                                WorkingStatusMenu:
                                int workingStatusMenuResult = AppMenu.PrintStatusMenu();
                                
                                switch (workingStatusMenuResult)
                                    {
                                        case 1:
                                            service.DisplayWorkingStatusActivityDetails(workingStatusId);
                                        goto WorkingStatusMenu;

                                        case 2:
                                            service.DisplayTodaysStatusActivitiesAndSummary();
                                        goto WorkingStatusMenu;

                                        case 3:
                                            service.EndWorkingStatusActivity(workingStatusId);//Press 1 to start tracking - Press 1 to select WORKING - Press 3 to change Status
                                        goto ChooseStatusMenu;

                                        case 0: //Press 1 to start tracking - Press 1 to select WORKING - Press 0 to stop tracking
                                            service.EndWorkingStatusActivity(workingStatusId);
                                        goto StartMenu;

                                        default:
                                            AppMenu.PrintInvalidOption();
                                        goto WorkingStatusMenu;
                                    }

                            case 2: //Press 1 to start tracking - Press 2 to select Break
                                int breakStatusId = service.CreateBreakStatusActivity();

                                BreakStatusMenu:
                                int breakStatusMenuResult = AppMenu.PrintStatusMenu();
                                    switch (breakStatusMenuResult)
                                    {
                                        case 1:
                                            service.DisplayBreakStatusActivityDetails(breakStatusId);
                                        goto BreakStatusMenu;

                                        case 2:
                                            service.DisplayTodaysStatusActivitiesAndSummary();
                                        goto BreakStatusMenu;

                                        case 3: //Press 1 to start tracking - Press 1 to select BREAK - Press 3 to change Status
                                            service.EndBreakStatusActivity(breakStatusId);
                                        goto ChooseStatusMenu;

                                        case 0: //Press 1 to start tracking - Press 1 to select BREAK - Press 0 to stop tracking
                                            service.EndBreakStatusActivity(breakStatusId);
                                        goto StartMenu; 

                                        default:
                                            AppMenu.PrintInvalidOption();
                                        goto BreakStatusMenu;

                                    }

                            case 3: //Press 1 to start tracking - Press 3 to select Away
                                int awayStatusId = service.CreateAwayStatusActivity();

                                AwayStatusMenu:
                                    int awayStatusMenuResult = AppMenu.PrintStatusMenu();
                                    switch (awayStatusMenuResult)
                                    {
                                        case 1:
                                            service.DisplayAwayStatusActivityDetails(awayStatusId);
                                        goto AwayStatusMenu;

                                        case 2:
                                            service.DisplayTodaysStatusActivitiesAndSummary();
                                        goto AwayStatusMenu;

                                        case 3: //Press 1 to start tracking - Press 1 to select AWAY - Press 3 to change Status
                                            service.EndAwayStatusActivity(awayStatusId);
                                        goto ChooseStatusMenu;

                                        case 0: //Press 1 to start tracking - Press 1 to select BREAK - Press 0 to stop tracking
                                            service.EndAwayStatusActivity(awayStatusId);
                                        goto StartMenu;

                                        default:
                                            AppMenu.PrintInvalidOption();
                                        goto AwayStatusMenu;

                                    }

                            case 0: //Press 1 to start tracking - Press 0 to  quit app
                                goto StartMenu;

                            default:
                                AppMenu.PrintInvalidOption();
                            goto ChooseStatusMenu;
                        }

                    case 0: //Press 0 to quit app
                        break;

                    case 2:
                        service.DisplayTodaysStatusActivitiesAndSummary();
                    goto StartMenu;

                    case 3:
                        service.DisplayLastSevenDaysSummary();
                    goto StartMenu;

                    default:
                        AppMenu.PrintInvalidOption();
                    goto StartMenu;
                }
            }
        }
    }
}