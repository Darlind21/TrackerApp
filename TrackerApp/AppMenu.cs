using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerApp
{
    public static class AppMenu
    {
        public static int PrintStartMenu()
        {
        PrintStartMenu:
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine
                (
                    "Press 1 to start tracking" + //triggers PrintChooseStatusMenu
                    //"\nPress 2 to display today's Status Activities and Summary" +
                    //"\nPress 3 to display current week's Summary"+
                    //"\nPress 4 to display current month's Summary" +
                    //"\nPress 5 to display all time Summary" +
                    //"\nPress 6 to select 1 custom date to to display its Status Activities and Summary" + //triggers PrintEnterCustomDate()
                    //"\nPress 7 to select custom dates to display Summary" + //triggers PrintEnterCustomDate() twice
                    "\nPress 0 to quit app " 
                );
            int result;
            bool isParsed= int.TryParse(Console.ReadLine(), out result);
            if (!isParsed)
            {
                PrintInvalidOption();
                goto PrintStartMenu;
            }
            return result;
            //if the TryParse() reads an empty string it returns false and sets result to 0 by default 
;        }

        public static void PrintEnterCustomDate()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter custom date");
        }

        public static void PrintInvalidOption()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nInvalid option\n");
        }

        public static int PrintChooseStatusMenu() 
        {
            PrintChooseStatusMenu:
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine
                (
                    "\nChoose status:" + 
                    "\nPress 1 to select WORKING" + //Triggers PrintStatusMenu()
                    "\nPress 2 to select BREAK" + //Triggers PrintStatusMenu()
                    "\nPress 3 to select AWAY" + //Triggers PrintStatusMenu()
                    "\nPress 0 to go to Start Menu" 
                );
            int result;
            bool isParsed = int.TryParse(Console.ReadLine(), out result);

            if (!isParsed)
            {
                PrintInvalidOption();
                goto PrintChooseStatusMenu; //if the input is not succesfully parse the method calls itself
            }
            return result;
        }

        public static int PrintStatusMenu()
        {
            PrintStatusMenu:
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine
                (
                    "\nTracking..." + //Maybe add current status name like Current Status: <StatusName>
                    "\nPress 1 to display current status details for today" +
                    //should display StartTime for current status activity for today
                    //should display duration for current status activity until now
                    //should display total duration for current status during the day 
                    //in the end it calls PrintStatusMenu()

                    "\nPress 2 to display today's Status Activities and Summary" +
                    "\nPress 3 to change Status" + //triggers PrintChooseStatusMenu()
                    "\nPress 0 to stop tracking" //triggers PrintStartMenu()
                );

            int result;
            bool isParsed = int.TryParse(Console.ReadLine(), out result);
            if (!isParsed)
            {
                PrintInvalidOption();
                goto PrintStatusMenu; //if the input is not succesfully parse the method calls itself
            }

            return result;

        }

    }
}
