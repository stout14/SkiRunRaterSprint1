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

        }

        public Controller(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        #endregion

        #region METHODS



        #endregion

    }
}
