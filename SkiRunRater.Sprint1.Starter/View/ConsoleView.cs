﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    public static class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        /// <summary>
        /// method to display the manager menu and get the user's choice
        /// </summary>
        /// <returns></returns>
        public static AppEnum.ManagerAction GetUserActionChoice()
        {
            AppEnum.ManagerAction userActionChoice = AppEnum.ManagerAction.None;
            //
            // set a string variable with a length equal to the horizontal margin and filled with spaces
            //
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            //
            // set up display area
            //
            DisplayReset();

            //
            // display the menu
            //
            DisplayMessage("Ski Manager Menu");
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "1. Display All Ski Runs" + Environment.NewLine +
                leftTab + "2. Display a Ski Run Detail" + Environment.NewLine +
                leftTab + "3. Add a Ski Run" + Environment.NewLine +
                leftTab + "4. Delete a Ski Run" + Environment.NewLine +
                leftTab + "5. Edit a Ski Run" + Environment.NewLine +
                leftTab + "6. Query Ski Runs by Vertical" + Environment.NewLine +
                leftTab + "E. Exit" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number/letter for the menu choice.");
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userActionChoice = AppEnum.ManagerAction.ListAllSkiRuns;
                    break;
                case '2':
                    userActionChoice = AppEnum.ManagerAction.DisplaySkiRunDetail;
                    break;
                case '3':
                    userActionChoice = AppEnum.ManagerAction.AddSkiRun;
                    break;
                case '4':
                    userActionChoice = AppEnum.ManagerAction.DeleteSkiRun;
                    break;
                case '5':
                    userActionChoice = AppEnum.ManagerAction.UpdateSkiRun;
                    break;
                case '6':
                    userActionChoice = AppEnum.ManagerAction.QuerySkiRunsByVertical;
                    break;
                case 'E':
                case 'e':
                    userActionChoice = AppEnum.ManagerAction.Quit;
                    break;
                default:
                    DisplayMessage("");
                    DisplayMessage("It appears you have selected an incorrect choice.");
                    DisplayMessage("");
                    DisplayMessage("Press any key to try again or the ESC key to exit.");


                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userActionChoice = AppEnum.ManagerAction.Quit;
                    }
                    break;
            }

            return userActionChoice;
        }

        /// <summary>
        /// gets a user ID from the user (either new or existing)
        /// </summary>
        /// <param name="skiRuns"></param>
        /// <param name="heading"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public static int GetSkiRunID(List<SkiRun> skiRuns, string heading, bool newID)
        {
            int skiRunID = -1;

            // variable used to store current y position in the console for user validation messages
            int currentConsoleY;

            DisplayAllSkiRuns(skiRuns, heading);

            currentConsoleY = Console.CursorTop;
            DisplayMessage("");
            DisplayPromptMessage("Enter the ski run ID: ");
            skiRunID = ConsoleUtil.ValidateSkiID("Please enter the ski run ID: ", Console.ReadLine(), skiRuns, newID, currentConsoleY);
            
            return skiRunID;
        }

        /// <summary>
        /// method to display all ski run info
        /// </summary>
        public static void DisplayAllSkiRuns(List<SkiRun> skiRuns, string heading)
        {
            DisplayReset();


            if (heading != "" || heading != null)
            {
                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center(heading, WINDOW_WIDTH));
                DisplayMessage("");
            }
            DisplayMessage("All of the existing ski runs are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in skiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));


                DisplayMessage(skiRunInfo.ToString());
            }
        }

        /// <summary>
        /// meethod to add a ski run to the data list
        /// </summary>
        /// <returns></returns>
        public static SkiRun AddSkiRun(List<SkiRun> skiRuns)
        {
            SkiRun skiRun = new SkiRun();

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Add A Ski Run", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayPromptMessage("Enter the ski run ID: ");
            skiRun.ID = ConsoleUtil.ValidateSkiID("Please enter the ski run ID: ", Console.ReadLine(), skiRuns, true, Console.CursorTop - 2);
            DisplayMessage("");

            DisplayPromptMessage("Enter the ski run name: ");
            skiRun.Name = Console.ReadLine();
            DisplayMessage("");

            DisplayPromptMessage("Enter the ski run vertical in feet: ");
            skiRun.Vertical = ConsoleUtil.ValidateIntegerResponse("Please the ski run vertical in feet: ", Console.ReadLine());

            return skiRun;
        }

        /// <summary>
        /// goes through the steps and view info to obtain a selected ski run to update
        /// </summary>
        /// <param name="skiRuns"></param>
        /// <returns></returns>
        public static SkiRun UpdateSkiRun(List<SkiRun> skiRuns)
        {
            SkiRun updatedRun = new SkiRun();
            int updatedRunID = GetSkiRunID(skiRuns, "Update A Ski Run", false);

            for (int index = 0; index < skiRuns.Count(); index++)
            {
                if (skiRuns[index].ID == updatedRunID)
                {
                    updatedRun = skiRuns[index];
                }
            }

            DisplayReset();

            DisplayMessage("Selected run info:");
            DisplayMessage("");
            DisplayMessage("Name: " + updatedRun.Name);
            DisplayMessage("Vertical feet: " + updatedRun.Vertical);
            DisplayMessage("");

            DisplayPromptMessage("Enter the updated ski run name: ");
            updatedRun.Name = Console.ReadLine();
            DisplayMessage("");

            DisplayPromptMessage("Enter the updated ski run vertical in feet: ");
            updatedRun.Vertical = ConsoleUtil.ValidateIntegerResponse("Please input the updated ski run vertical in feet: ", Console.ReadLine());

            return updatedRun;
        }

        /// <summary>
        /// displays detailed ski run info
        /// </summary>
        /// <param name="skiRun"></param>
        public static void DisplaySkiRun(SkiRun skiRun)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Ski Run Detail", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Ski Run: {0}", skiRun.Name));
            DisplayMessage("");

            DisplayMessage(String.Format("ID: {0}", skiRun.ID.ToString()));
            DisplayMessage(String.Format("Vertical in Feet: {0}", skiRun.Vertical.ToString()));

            DisplayMessage("");
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// displays the continue prompt with a custom message
        /// </summary>
        /// <param name="customMessage"></param>
        public static void DisplayContinuePrompt(string customMessage)
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center(customMessage, WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ResetColor();
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }

        public static void GetVerticalQueryMinMaxValues(out int minimumVertical, out int maximumVertical)
        {
            minimumVertical = 0;
            maximumVertical = 0;
            string userResponse = "";

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Query Ski Runs by Vertical", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayPromptMessage("Enter the minimum vertical: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                minimumVertical = ConsoleUtil.ValidateIntegerResponse("Please enter the minimum vertical in feet.", userResponse);
            }

            DisplayMessage("");

            DisplayPromptMessage("Enter the maximum vertical: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                maximumVertical = ConsoleUtil.ValidateIntegerResponse("Please enter the maximum vertical in feet.", userResponse);
            }

            DisplayMessage("");

            DisplayMessage(String.Format("You have entered {0} feet as the minimum value and {1} as the maximum value.", minimumVertical, maximumVertical));

            DisplayMessage("");

            DisplayContinuePrompt();
        }

        public static void DisplayQueryResults(List<SkiRun> matchingSkiRuns)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Display Ski Run Query Results", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage("All of the matching ski runs are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in matchingSkiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));

                DisplayMessage(skiRunInfo.ToString());
            }
        }


        #endregion
    }
}
