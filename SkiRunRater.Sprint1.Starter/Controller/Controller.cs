﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            SkiRunRepository skiRunRepository = new SkiRunRepository();
                        
            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.GetAllSkiRuns();

                int skiRunID;
                SkiRun skiRun;

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;

                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            ConsoleView.DisplayAllSkiRuns(skiRuns, "");
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns, "Get Detailed Ski Run Info", false);
                            skiRun = skiRunRepository.GetSkiRunByID(skiRunID);
                            ConsoleView.DisplaySkiRun(skiRun);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DeleteSkiRun:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns, "Delete Ski Run Record", false);
                            skiRunRepository.DeleteSkiRun(skiRunID);
                            ConsoleView.DisplayContinuePrompt("Run Deleted");
                            break;

                        case AppEnum.ManagerAction.AddSkiRun:
                            skiRun = ConsoleView.AddSkiRun(skiRuns);
                            skiRunRepository.InsertSkiRun(skiRun);
                            ConsoleView.DisplayContinuePrompt("Run Added");
                            break;

                        case AppEnum.ManagerAction.UpdateSkiRun:
                            skiRun = ConsoleView.UpdateSkiRun(skiRuns);
                            skiRunRepository.UpdateSkiRun(skiRun);
                            ConsoleView.DisplayContinuePrompt("Run Updated");
                            break;

                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            break;

                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;

                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
