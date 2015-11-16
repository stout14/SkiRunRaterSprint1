using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region ENUMERABLES


        #endregion


        #region FIELDS


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
            ConsoleView.DisplayWelcomeScreen();

            ConsoleView.DisplaySkiRuns();

            SkiRunRepository srr = new SkiRunRepository();

            using (srr)
            {
                srr.DeleteSkiRun(1);
            }

            ConsoleView.DisplaySkiRuns();
        }

        #endregion

    }
}
