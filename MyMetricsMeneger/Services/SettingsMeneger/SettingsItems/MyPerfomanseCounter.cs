using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMetricsMeneger
{
    public class MyPerfomanseCounter
    {
        public int _frequencyCounter { get; set; }
        public bool _enabled { get; set; }
        public PerformanceCounter _performanceCounter { get; set; }
    }
}
