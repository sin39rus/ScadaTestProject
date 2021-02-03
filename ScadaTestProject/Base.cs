using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaTestProject
{
    public class Base
    {
        public string Name { get; set; }
        public DateTime PlannedStart { get; set; }
        public DateTime PlannedEnd { get; set; }
        public byte Completed { get; set; }
    }
}
