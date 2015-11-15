using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    class Program
    {
        static void Main(string[] args)
        {
            // add test data to the data file
            InitializeDataFile.AddTestData();

            // instantiate the controller
            Controller appContoller = new Controller();
        }

        //static void DemoSkiRunReadWrite(string dataFile)
        //{
        //    List<SkiRun> SkiRunClassListWrite = new List<SkiRun>();

        //    List<string> SkiRunStringListRead = new List<string>(); ;
        //    List<SkiRun> SkiRunClassListRead = new List<SkiRun>(); ;

        //    // initialize a list of SkiRun objects
        //    SkiRunClassListWrite = InitializeListOfSkiRunInfo();

        //    Console.WriteLine("The following ski runs will be added to Data.txt.\n");
        //    // display list of high scores objects
        //    DisplaySkiRuns(SkiRunClassListWrite);

        //    Console.WriteLine("\nAdd ski runs to text file. Press any key to continue.\n");
        //    Console.ReadKey();

        //    // build the list of strings and write to the text file line by line
        //    SkiRunRepository.WriteAllSkiRunsToTextFile(SkiRunClassListWrite, dataFile);

        //    Console.WriteLine("Ski runs added successfully.\n");

        //    Console.WriteLine("Read into a string of ski runs and display the ski run info from data file. Press any key to continue.\n");
        //    Console.ReadKey();


        //    // build the list of SkiRun class objects from the list of strings
        //    SkiRunClassListRead = SkiRunRepository.ReadAllSkiRunsFromTextFile(dataFile);

        //    // display list of ski run objects
        //    DisplaySkiRuns(SkiRunClassListRead);
        //}



    }
}
