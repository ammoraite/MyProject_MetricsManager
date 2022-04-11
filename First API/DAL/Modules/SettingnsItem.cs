using System.Diagnostics;

namespace MetricsMeneger.DAL.Modules
{
    public class SettingnsItem
    {
        public PerformanceCounter _counter { get; set; }
        public bool _doOrNot { get; set; }
        public SettingnsItem( string category,string instanse,string counter, bool DoOrNot)
        {
            _counter = new(category, counter, instanse);
            
            _doOrNot = DoOrNot;
        }
    }
}
