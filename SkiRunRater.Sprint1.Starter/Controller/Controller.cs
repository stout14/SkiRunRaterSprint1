using System;
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
                string message;

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;

                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            ConsoleView.DisplayAllSkiRuns(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRun = skiRunRepository.GetSkiRunByID(skiRunID);
                            ConsoleView.DisplaySkiRun(skiRun);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DeleteSkiRun:
                            //TODO add console view functionality for deleting ski run
                            skiRunRepository.DeleteSkiRun(1);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.AddSkiRun:
                            skiRun = ConsoleView.AddSkiRun();
                            skiRunRepository.InsertSkiRun(skiRun);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.UpdateSkiRun:
                            skiRun = ConsoleView.UpdateSkiRun(skiRunRepository.GetAllSkiRuns());
                            skiRunRepository.UpdateSkiRun(skiRun);
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
