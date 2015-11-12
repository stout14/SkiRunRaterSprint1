using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class SkiRun
    {
        public string Name { get; set; }
        public int Vertical { get; set; }

        public SkiRun()
        {

        }

        public SkiRun(string Name, int vertical)
        {
            this.Name = Name;
            this.Vertical = vertical;
        }
    }
}
