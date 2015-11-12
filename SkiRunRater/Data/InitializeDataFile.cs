using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class InitializeDataFile
    {

        public static void AddTestData()
        {
            List<SkiRun> skiRuns = new List<SkiRun>();

            // initialize the IList of high scores - note: no instantiation for an interface
            skiRuns.Add(new SkiRun() { Name = "Buck", Vertical = 325 });
            skiRuns.Add(new SkiRun() { Name = "Buckaroo", Vertical = 325 });
            skiRuns.Add(new SkiRun() { Name = "Hoot Owl", Vertical = 325 });
            skiRuns.Add(new SkiRun() { Name = "Shelburg's Chute", Vertical = 325 });

            CRUDSkiRun.WriteAllSkiRunsToTextFile(skiRuns, DataSettings.dataFilePath);
        }
    }
}
