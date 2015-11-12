using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class InitializeDataFile
    {

        public static void AddTestData(string dataFilePath)
        {
            List<SkiRun> skiRunList = new List<SkiRun>();

            // initialize the IList of high scores - note: no instantiation for an interface
            skiRunList.Add(new SkiRun() { Name = "Buck", Vertical = 325 });
            skiRunList.Add(new SkiRun() { Name = "Buckaroo", Vertical = 325 });
            skiRunList.Add(new SkiRun() { Name = "Hoot Owl", Vertical = 325 });
            skiRunList.Add(new SkiRun() { Name = "Shelburg's Chute", Vertical = 325 });

            CRUDSkiRun.WriteAllSkiRunsToTextFile(skiRunList, dataFilePath);
        }
    }
}
