using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkiRunRater
{
    /// <summary>
    /// class to hold utility methods for the console
    /// </summary>
    public static class ConsoleUtil
    {
        #region FIELDS

        private static int _promptLocationX = 3;

        #endregion

        /// <summary>
        /// wraps text using a list of strings
        /// Original code from Mike Ward's website
        /// http://mike-ward.net/2009/07/19/word-wrap-in-a-console-app-c/
        /// Adapted to include a left margin for console window support
        /// </summary>
        /// <param name="text">text to wrap</param>
        /// <param name="rightMargin">length of each line</param>
        /// <param name="leftMargin">left margin</param>
        /// <returns>list of lines as strings</returns>
        public static List<string> Wrap(string text, int rightMargin, int leftMargin)
        {
            int start = 0, end;
            var lines = new List<string>();

            string leftMarginSpaces = "";
            for (int i = 0; i < leftMargin; i++)
            {
                leftMarginSpaces += " ";
            }

            text = Regex.Replace(text, @"\s", " ").Trim();

            while ((end = start + rightMargin) < text.Length)
            {
                while (text[end] != ' ' && end > start)
                    end -= 1;

                if (end == start)
                    end = start + rightMargin;

                lines.Add(leftMarginSpaces + text.Substring(start, end - start));
                start = end + 1;
            }

            if (start < text.Length)
                lines.Add(leftMarginSpaces + text.Substring(start));

            return lines;
        }

        /// <summary>
        /// center text as a function of the window width with padding on both sides
        /// Note: the method currently assumes the text will fit on one line
        /// </summary>
        /// <param name="text">text to center</param>
        /// <param name="windowWidth">the width of the window in characters</param>
        /// <returns>string with spaces and centered text</returns>
        public static string Center(string text, int windowWidth)
        {
            int leftPadding = (windowWidth - text.Length) / 2 + text.Length;
            return text.PadLeft(leftPadding).PadRight(windowWidth);
        }

        /// <summary>
        /// generate a string of spaces of a given length
        /// </summary>
        /// <param name="stringLength">length of string</param>
        /// <returns>string of spaces</returns>
        public static string FillStringWithSpaces(int stringLength)
        {
            string rowOfSpaces = "";
            return rowOfSpaces.PadRight(stringLength);
        }

        /// <summary>
        /// convert camel-case to all upper case and spaces
        /// reference URL - http://stackoverflow.com/questions/15458257/how-to-have-enum-values-with-spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String ToLabelFormat(String s)
        {
            var newStr = Regex.Replace(s, "(?<=[A-Z])(?=[A-Z][a-z])", " ");
            newStr = Regex.Replace(newStr, "(?<=[^A-Z])(?=[A-Z])", " ");
            //newStr = Regex.Replace(newStr, "(?<=[A-Za-z])(?=[^A-Za-z])", " ");

            return newStr;
        }

        /// <summary>
        /// check for a valid integer from user input
        /// </summary>
        /// <param name="promptMessage"></param>
        /// <param name="userResponse"></param>
        /// <param name="consoleYMessageStart"></param>
        /// <returns></returns>
        public static int ValidateIntegerResponse(string promptMessage, string userResponse)
        {
            int userResponseInteger = -1;

            while (!(int.TryParse(userResponse, out userResponseInteger)))
            {
                ConsoleView.DisplayReset();

                ConsoleView.DisplayMessage("");
                ConsoleView.DisplayMessage("It appears you have not entered a valid integer.");

                ConsoleView.DisplayMessage("");
                ConsoleView.DisplayPromptMessage(promptMessage);
                userResponse = Console.ReadLine();


            }


            return userResponseInteger;
        }

        /// <summary>
        /// evaluates a user input to make sure it is a valid ski run ID (either as a new entry or existing)
        /// </summary>
        /// <param name="promptMessage"></param>
        /// <param name="userResponse"></param>
        /// <param name="skiRuns"></param>
        /// <param name="newID"></param>
        /// <param name="consoleYMessageStart"></param>
        /// <returns></returns>
        public static int ValidateSkiID(string promptMessage, string userResponse, List<SkiRun> skiRuns, bool newID, int consoleYMessageStart)
        {
            int userSkiID = -1;

            List<int> possibleIDs = new List<int>();

            foreach (var run in skiRuns)
            {
                possibleIDs.Add(run.ID);
            }

            // change invalid entry guidance text based on whether or not a new ski run is being requested
            string invalidEntryText = "It appears you have not entered a valid ski run ID.";

            if (newID)
            { invalidEntryText = "It appears you have not entered a valid or new ski run ID."; }

            // evalutes to see if user entry is a valid integer, then based on whether a new or old id is required, if a correct ID
            // number was given
            while ( !(int.TryParse(userResponse, out userSkiID)) || 
                    (!possibleIDs.Contains(userSkiID) && !newID) || 
                    (possibleIDs.Contains(userSkiID) && newID)  )
            {
                Console.SetCursorPosition(_promptLocationX, consoleYMessageStart);

                ConsoleView.DisplayMessage("");
                ConsoleView.DisplayMessage(invalidEntryText);

                ConsoleView.DisplayMessage("");
                ConsoleView.DisplayPromptMessage(promptMessage);
                ConsoleView.DisplayMessage("");

                // erase previous response
                Console.WriteLine(Center("  ", Console.WindowWidth));

                Console.SetCursorPosition(0 + 3, Console.CursorTop - 1);
                userResponse = Console.ReadLine();
            }

            return userSkiID;
        }
    }
}
