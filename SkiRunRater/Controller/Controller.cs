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

        private string _dataFilePath;
        List<SkiRun> _skiRuns = new List<SkiRun>();

        #endregion

        #region PROPERTIES

        public string DataFilePath
        {
            get { return _dataFilePath; }
            set { _dataFilePath = value; }
        }

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

            _skiRuns = CRUDSkiRun.ReadAllSkiRunsFromTextFile(_dataFilePath);

            ConsoleView.DisplaySkiRuns();
        }

        #endregion

    }
}
