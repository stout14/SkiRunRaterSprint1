using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration.Assemblies;


namespace SkiRunRater
{
    public class SkiRunRepositorySQL_bk : ISkiRunRepository
    {
        //DataSet _skiRuns_ds = new DataSet();
        //DataTable _skiRuns_dt = new DataTable();

        public SkiRunRepositorySQL_bk()
        {

        }


        /// <summary>
        /// method to return a ski run object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun SelectSkiRun(int ID)
        {
            DataTable skiRuns_dt = new DataTable();
            DataRow skiRunRow;
            SkiRun skiRun = new SkiRun();

            string connString = GetConnectionString();
            string sqlCommandString = String.Format("SELECT * from SkiRuns WHERE ID = " + ID);

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn);
            SqlDataAdapter sqlAdater = new SqlDataAdapter(sqlCommand);

            using (sqlConn)
            {
                using (sqlCommand)
                {
                    try
                    {
                        sqlConn.Open();
                        sqlAdater.Fill(skiRuns_dt);
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    }
                }
            }

            if (skiRuns_dt.Rows.Count == 1)
            {
                skiRunRow = skiRuns_dt.Rows[0];

                skiRun.ID = int.Parse(skiRunRow["ID"].ToString());
                skiRun.Name = skiRunRow["Name"].ToString();
                skiRun.Vertical = int.Parse(skiRunRow["Vertical"].ToString());
            }
            else
            {
                // ski run ID not unique
            }

            return skiRun;
        }

        /// <summary>
        /// method to return a ski run object given the name
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun SelectSkiRun(string name)
        {
            DataTable skiRuns_dt = new DataTable();
            DataRow skiRunRow;
            SkiRun skiRun = new SkiRun();

            string connString = GetConnectionString();
            string sqlCommandString = String.Format("SELECT * from SkiRuns WHERE Name = " + name.ToString());

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn);
            SqlDataAdapter sqlAdater = new SqlDataAdapter(sqlCommand);

            using (sqlConn)
            {
                using (sqlCommand)
                {
                    try
                    {
                        sqlConn.Open();
                        sqlAdater.Fill(skiRuns_dt);
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    }
                }
            }

            if (skiRuns_dt.Rows.Count == 1)
            {
                skiRunRow = skiRuns_dt.Rows[0];

                skiRun.ID = int.Parse(skiRunRow["ID"].ToString());
                skiRun.Name = skiRunRow["Name"].ToString();
                skiRun.Vertical = int.Parse(skiRunRow["Vertical"].ToString());
            }
            else
            {
                // ski run ID not unique
            }

            return skiRun;
        }

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> SelectAllSkiRuns()
        {
            DataTable skiRuns_dt = new DataTable();
            List<SkiRun> skiRuns = new List<SkiRun>();

            string connString = GetConnectionString();
            string sqlCommandString = "SELECT * from SkiRuns";

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn);
            SqlDataAdapter sqlAdater = new SqlDataAdapter(sqlCommand);

            using (sqlConn)
            {
                using (sqlCommand)
                {
                    try
                    {
                        sqlConn.Open();
                        sqlAdater.Fill(skiRuns_dt);
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    }
                }
            }

            foreach (DataRow skiRun in skiRuns_dt.Rows)
            {
                skiRuns.Add(new SkiRun
                {
                    ID = int.Parse(skiRun["ID"].ToString()),
                    Name = skiRun["Name"].ToString(),
                    Vertical = int.Parse(skiRun["Vertical"].ToString()),
                });
            }

            return skiRuns;
        }

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> SelectSkiRuns(string sqlCommandString)
        {
            DataTable skiRuns_dt = new DataTable();
            List<SkiRun> skiRuns = new List<SkiRun>();

            string connString = GetConnectionString();

            SqlConnection sqlConn = new SqlConnection(connString);
            SqlCommand sqlCommand = new SqlCommand(sqlCommandString, sqlConn);
            SqlDataAdapter sqlAdater = new SqlDataAdapter(sqlCommand);

            using (sqlConn)
            {
                using (sqlCommand)
                {
                    try
                    {
                        sqlConn.Open();
                        sqlAdater.Fill(skiRuns_dt);
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("SQL Exception: {0}", sqlEx.Message);
                    }
                }
            }

            foreach (DataRow skiRun in skiRuns_dt.Rows)
            {
                skiRuns.Add(new SkiRun
                {
                    ID = int.Parse(skiRun["ID"].ToString()),
                    Name = skiRun["Name"].ToString(),
                    Vertical = int.Parse(skiRun["Vertical"].ToString()),
                });
            }

            return skiRuns;
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void InsertSkiRun(SkiRun skiRun)
        {
            DataSet skiRuns_ds = new DataSet();
            DataTable skiRuns_dt = new DataTable();

            fillRow(skiRuns_dt, skiRun.ID, skiRun.Name, skiRun.Vertical);
        }

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteSkiRun(int ID)
        {
            DataTable skiRuns_dt = new DataTable();
            DataRow[] skiRuns;

            // get an array of ski runs with the matching ID
            skiRuns = skiRuns_dt.Select("ID = " + ID.ToString());

            // no ski runs with the matching ID
            if (skiRuns.Count() < 1)
            {
                throw new Exception("Ski run now found.");
            }
            // multiple ski runs with the matching ID
            else if (skiRuns.Count() > 1)
            {
                throw new Exception("More than one ski run with the id: " + ID);
            }
            // a unique ski run with the matching ID
            else
            {
                skiRuns_dt.Rows.Remove(skiRuns[0]);
            }
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void UpdateSkiRun(SkiRun skiRun)
        {
            DataTable skiRuns_dt = new DataTable();
            DataRow[] skiRuns;
            int ID = skiRun.ID;

            // get an array of ski runs with the matching ID
            skiRuns = skiRuns_dt.Select("ID = " + ID.ToString());

            // no ski runs with the matching ID
            if (skiRuns.Count() < 1)
            {
                throw new Exception("Ski run now found.");
            }
            // multiple ski runs with the matching ID
            else if (skiRuns.Count() > 1)
            {
                throw new Exception("More than one ski run with the id: " + ID);
            }
            // a unique ski run with the matching ID
            else
            {
                skiRuns[0]["ID"] = skiRun.ID;
                skiRuns[0]["Name"] = skiRun.Name;
                skiRuns[0]["Vertical"] = skiRun.Vertical;
            }
        }

        /// <summary>
        /// method to query the data by the vertical of each ski run in feet
        /// </summary>
        /// <param name="minimumVertical">int minimum vertical</param>
        /// <param name="maximumVertical">int maximum vertical</param>
        /// <returns></returns>
        public List<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            DataTable skiRuns_dt = new DataTable();

            // create a list to hold the matching ski runs
            List<SkiRun> matchingSkiRuns = new List<SkiRun>();

            // create an array of DataRows to hold the matching ski runs
            DataRow[] skiRuns;

            // generate the a query string based on the values of the arguments sent
            string sqlCommandString = String.Format("Vertical > {0} AND Vertical < {1}", minimumVertical, maximumVertical);

            // use the Select method to fill the array of ski runs
            skiRuns = skiRuns_dt.Select(sqlCommandString);

            // for each row in the array, create a new SkiRun object and add it to the list
            foreach (DataRow skiRun in skiRuns)
            {
                matchingSkiRuns.Add(new SkiRun()
                {
                    ID = int.Parse(skiRun["ID"].ToString()),
                    Name = skiRun["Name"].ToString(),
                    Vertical = int.Parse(skiRun["Vertical"].ToString()),
                });
            }

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
        /// get the connection string by name
        /// </summary>
        /// <returns>string connection string</returns>
        private static string GetConnectionString()
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            var settings = ConfigurationManager.ConnectionStrings["SkiRunRater"];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {

        }
    }
}
