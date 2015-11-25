using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SkiRunRater
{
    /// <summary>
    /// method to write all ski run information to the date file
    /// </summary>
    public class SkiRunRepositoryXML_DS : IDisposable
    {
        DataSet _skiRuns_ds = new DataSet();
        DataTable _skiRuns_dt = new DataTable();

        public SkiRunRepositoryXML_DS()
        {
            _skiRuns_ds = ReadSkiRunsData(DataSettings.dataFilePath);
            _skiRuns_ds.DataSetName = "SkiRuns";
            _skiRuns_dt = _skiRuns_ds.Tables[0];
        }

        /// <summary>
        /// method to read all ski run information from the XML data file and return it as a list of SkiRun objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of SkiRun objects</returns>
        public DataSet ReadSkiRunsData(string dataFilePath)
        {
            //SkiRuns skiRunsFromFile = new SkiRuns();

            XmlReader xmlFile;
            xmlFile = XmlReader.Create(DataSettings.dataFilePath);

            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);

            return ds;
        }

        /// <summary>
        /// method to write all of the list of ski runs to the text file
        /// </summary>
        public void WriteSkiRunsData()
        {
            // name the DataSet and write it to the XML file
            _skiRuns_ds.WriteXml(DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void InsertSkiRun(SkiRun skiRun)
        {
            List<SkiRun> skiRuns = new List<SkiRun>();

            skiRuns.Add(skiRun);

            WriteSkiRunsData();
        }

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteSkiRun(int ID)
        {
            List<SkiRun> skiRuns = new List<SkiRun>();

            skiRuns.RemoveAt(GetSkiRunIndex(ID));

            WriteSkiRunsData();
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void UpdateSkiRun(SkiRun skiRun)
        {
            DeleteSkiRun(skiRun.ID);
            InsertSkiRun(skiRun);

            WriteSkiRunsData();
        }

        /// <summary>
        /// method to return a ski run object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun GetSkiRunByID(int ID)
        {
            SkiRun skiRun = null;

            //skiRun = _skiRuns[GetSkiRunIndex(ID)];

            return skiRun;
        }

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> GetSkiAllRuns()
        {
            List<SkiRun> skiRuns = new List<SkiRun>();

            foreach (DataRow skiRun in _skiRuns_dt.Rows)
            {
                skiRuns.Add(new SkiRun {
                    ID = int.Parse(skiRun["ID"].ToString()),
                    Name = skiRun["Name"].ToString(),
                    Vertical = int.Parse(skiRun["Vertical"].ToString()),
                });
            }

            return skiRuns;
        }

        /// <summary>
        /// method to return the index of a given ski run
        /// </summary>
        /// <param name="skiRun"></param>
        /// <returns>int ID</returns>
        private int GetSkiRunIndex(int ID)
        {
            int skiRunIndex = 0;

            //for (int index = 0; index < _skiRuns.Count(); index++)
            //{
            //    if (_skiRuns[index].ID == ID)
            //    {
            //        skiRunIndex = index;
            //    }
            //}

            return skiRunIndex;
        }

        /// <summary>
        /// method to query the data by the vertical of each ski run in feet
        /// </summary>
        /// <param name="minimumVertical">int minimum vertical</param>
        /// <param name="maximumVertical">int maximum vertical</param>
        /// <returns></returns>
        public List<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            List<SkiRun> matchingSkiRuns = new List<SkiRun>();

            //foreach (var skiRun in _skiRuns)
            //{
            //    if ((skiRun.Vertical >= minimumVertical) && (skiRun.Vertical <= maximumVertical))
            //    {
            //        matchingSkiRuns.Add(skiRun);
            //    }
            //}

            return matchingSkiRuns;
        }

        /// <summary>
        /// add a row of data to the DataTable
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="ID">ski run ID</param>
        /// <param name="name">ski run Name</param>
        /// <param name="vertical">ski run vertical</param>
        private static void fillRow(DataTable dt, int ID, string name, int vertical)
        {
            DataRow dr;
            dr = dt.NewRow();

            dr["ID"] = ID;
            dr["Name"] = name;
            dr["Vertical"] = vertical;

            dt.Rows.Add(dr);
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _skiRuns_ds.Dispose();
        }
    }
}
