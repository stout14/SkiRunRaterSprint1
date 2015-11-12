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
            string dataFilePath = "Data\\Data.txt";

            Controller appContoller = new Controller(dataFilePath);


            //DemoSkiRunReadWrite(dataFilePath);

            //Console.WriteLine("\nPress any key to exit.");
            //Console.ReadKey();
        }

        static void DemoSkiRunReadWrite(string dataFile)
        {
            List<SkiRun> SkiRunClassListWrite = new List<SkiRun>();

            List<string> SkiRunStringListRead = new List<string>(); ;
            List<SkiRun> SkiRunClassListRead = new List<SkiRun>(); ;

            // initialize a list of SkiRun objects
            SkiRunClassListWrite = InitializeListOfSkiRunInfo();

            Console.WriteLine("The following ski runs will be added to Data.txt.\n");
            // display list of high scores objects
            DisplaySkiRuns(SkiRunClassListWrite);

            Console.WriteLine("\nAdd ski runs to text file. Press any key to continue.\n");
            Console.ReadKey();

            // build the list of strings and write to the text file line by line
            CRUDOperations.WriteAllSkiRunsToTextFile(SkiRunClassListWrite, dataFile);

            Console.WriteLine("Ski runs added successfully.\n");

            Console.WriteLine("Read into a string of ski runs and display the ski run info from data file. Press any key to continue.\n");
            Console.ReadKey();


            // build the list of SkiRun class objects from the list of strings
            SkiRunClassListRead = CRUDOperations.ReadAllSkiRunsFromTextFile(dataFile);

            // display list of ski run objects
            DisplaySkiRuns(SkiRunClassListRead);
        }

        static List<SkiRun> InitializeListOfSkiRunInfo()
        {
            List<SkiRun> skiRunList = new List<SkiRun>();

            // initialize the IList of high scores - note: no instantiation for an interface
            skiRunList.Add(new SkiRun() { Name = "Buck", Vertical = 325 });
            skiRunList.Add(new SkiRun() { Name = "Buckaroo", Vertical = 325 });
            skiRunList.Add(new SkiRun() { Name = "Hoot Owl", Vertical = 325 });
            skiRunList.Add(new SkiRun() { Name = "Shelburg's Chute", Vertical = 325 });

            return skiRunList;
        }

        static void DisplaySkiRuns(List<SkiRun> SkiRunClassList)
        {
            foreach (SkiRun skiRun in SkiRunClassList)
            {
                Console.WriteLine("Ski Run: {0}\tVertical: {1}", skiRun.Name, skiRun.Vertical);
            }
        }
    }
}
