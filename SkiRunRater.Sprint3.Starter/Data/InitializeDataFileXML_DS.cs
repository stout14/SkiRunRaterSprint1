using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkiRunRater
{
    public class InitializeDataFileXML_DS
    {

        public static void AddTestData()
        {
            // declare DataSet and DataTable
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            // declare the name and type of each column
            dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Name", typeof(String)));
            dt.Columns.Add(new DataColumn("Vertical", typeof(Int32)));

            // add test data to each row in the DataTable
            fillRow(dt, 1, "Buck", 200);
            fillRow(dt, 2, "Buckaroo", 325);
            fillRow(dt, 3, "Hoot Ow", 999);
            fillRow(dt, 4, "Shelburg's Chute", 1023);

            // add the DataTable to the DataSet and name it
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "SkiRun";

            // name the DataSet and write it to the XML file
            ds.DataSetName = "SkiRuns";
            ds.WriteXml(DataSettings.dataFilePath);
        }

        /// <summary>
        /// add a row of data to the datatable
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
    }
}

