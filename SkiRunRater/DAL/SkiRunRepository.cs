using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    /// <summary>
    /// method to write all ski run information to the date file
    /// </summary>
    public class SkiRunRepository : IDisposable
    {
        private List<SkiRun> _skiRuns;

        public SkiRunRepository()
        {
            _skiRuns = ReadSkiRunsData(DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to read all ski run information from the data file and return it as a list of SkiRun objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of SkiRun objects</returns>
        public static List<SkiRun> ReadSkiRunsData(string dataFilePath)
        {
            const char delineator = ',';

            // create lists to hold the ski run strings and objects
            List<string> skiRunStringList = new List<string>();
            List<SkiRun> skiRunClassList = new List<SkiRun>();

            // initialize a FileStream object for reading
            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    skiRunStringList.Add(sReader.ReadLine());
                }
            }


            foreach (string skiRun in skiRunStringList)
            {
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = skiRun.Split(delineator);

                // populate the ski run list with SkiRun objects
                skiRunClassList.Add(new SkiRun() { ID = Convert.ToInt32(properties[0]), Name = properties[1], Vertical = Convert.ToInt32(properties[2]) });
            }

            return skiRunClassList;
        }

        //public static List<SkiRun> ReadSkiRunsData(string dataFilePath)
        //{
        //    const char delineator = ',';

        //    // create lists to hold the ski run strings and objects
        //    List<string> skiRunStringList = new List<string>();
        //    List<SkiRun> skiRunClassList = new List<SkiRun>();

        //    // initialize a FileStream object for reading
        //    FileStream rFileStream = File.OpenRead(DataSettings.dataFilePath);

        //    // wrap the FieldStream object in a using statement to ensure of the dispose
        //    using (rFileStream)
        //    {
        //        // initialize a FileStream object for reading
        //        StreamReader sReader = new StreamReader(rFileStream);

        //        // keep reading lines of text until the end of the file is reached
        //        while (!sReader.EndOfStream)
        //        {
        //            skiRunStringList.Add(sReader.ReadLine());
        //        }
        //    }

        //    foreach (string skiRun in skiRunStringList)
        //    {
        //        // use the Split method and the delineator on the array to separate each property into an array of properties
        //        string[] properties = skiRun.Split(delineator);

        //        // populate the ski run list with SkiRun objects
        //        skiRunClassList.Add(new SkiRun() { ID = Convert.ToInt32(properties[0]), Name = properties[1], Vertical = Convert.ToInt32(properties[2]) });
        //    }

        //    return skiRunClassList;
        //}

        //public void WriteSkiRunData()
        //{
        //    string skiRunString;

        //    // initialize a FileStream object for writing
        //    FileStream wfileStream = File.OpenWrite(DataSettings.dataFilePath, false);

        //    // wrap the FieldStream object in a using statement to ensure of the dispose
        //    using (wfileStream)
        //    {
        //        // wrap the FileStream object in a StreamWriter object to simplify writing strings
        //        StreamWriter sWriter = new StreamWriter(wfileStream);

        //        foreach (SkiRun skiRun in _skiRuns)
        //        {
        //            skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

        //            sWriter.WriteLine(skiRunString);
        //        }

        //        // be sure to close the StreamWriter object

        //       sWriter.Close();
        //    }

        //}

        public void WriteSkiRunData()
        {
            string skiRunString;

            // wrap the FileStream object in a StreamWriter object to simplify writing strings
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath, false);

            using (sWriter)
            {
                foreach (SkiRun skiRun in _skiRuns)
                {
                    skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

                    sWriter.WriteLine(skiRunString);
                }
            }
        }

        public static void InsertSkiRun(SkiRun skiRun)
        {
            string skiRunString;

            skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

            // initialize a FileStream object for writing
            FileStream wfileStream = File.OpenWrite(DataSettings.dataFilePath);

            // wrap the FieldStream object in a using statement to ensure of the dispose
            using (wfileStream)
            {
                // wrap the FileStream object in a StreamWriter object to simplify writing strings
                StreamWriter sWriter = new StreamWriter(wfileStream);

                sWriter.WriteLine(skiRunString);

                // be sure to close the StreamWriter object
                sWriter.Close();
            }

        }

        public void DeleteSkiRun(int ID)
        {
            for (int index = 0; index < _skiRuns.Count(); index++)
            {
                if (_skiRuns[index].ID == ID)
                {
                    _skiRuns.RemoveAt(index);
                }
            }

            WriteSkiRunData();
        }

        public void Dispose()
        {

        }
    }
}
