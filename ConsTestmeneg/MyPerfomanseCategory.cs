using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsTestmeneg
{
    public class MyPerfomanseCategory
    {
        public string _categoryName { get; set; }
        public bool _enabled { get; set; }
        public List<MyPerfomanseCounter> _performanceCounters = new List<MyPerfomanseCounter>();
    }
}
