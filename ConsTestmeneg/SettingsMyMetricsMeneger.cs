using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsTestmeneg
{
    public class SettingsMyMetricsMeneger
    {
        private bool _updatingSettings { get; set; }
        public string _configFilePath
        {
            get
            {
                return _configFilePath;
            }
            private set
            {
                if (_settingsWorker.CheckNewPathAndSetinWorker(value))
                {
                    _configFilePath = value;
                }
                else
                {
                    _configFilePath = "MyMetricsMenegerConfig.json";
                }
            }
        }

        public List<MyPerfomanseCategory> _myPerfomanseMetricsToDo { get; private set; }

        private SettingsWorker _settingsWorker;
        public SettingsMyMetricsMeneger()
        {
            _configFilePath = "MyMetricsMenegerConfig.json";
            _settingsWorker = new SettingsWorker(_configFilePath);
            _myPerfomanseMetricsToDo = _settingsWorker.GetListFarmerConfigs();
        }

        /// <summary>Изменяет путь к файлу конфигураций</summary>
        /// <param name="newconfigFilePass"></param>
        public void UpDateConfigFilePath(string newconfigFilePass)
        {
            _configFilePath = newconfigFilePass;
        }


        /// <summary>
        /// Вносит изменения какие категории будут работать
        /// </summary>
        /// <param name="nameCategory"></param>
        /// <param name="doCategory"></param>
        public void UpDateConfigCategoryMetrics(string nameCategory,bool doCategory)
        {
            _myPerfomanseMetricsToDo = _settingsWorker.UpDateConfigCategoryMetrics(nameCategory, doCategory);
        }

        /// <summary>
        /// Вносит изменения какие метрики будут работать и с какой частотой(частота - необязательный параметр)
        /// </summary>
        /// <param name="nameСounter"></param>
        /// <param name="doCategory"></param>
        public void UpDateConfigСounterMetrics(string nameСounter, bool doCounter, int frequencyCounter = -1)
        {
            if (frequencyCounter==-1)
            {
                _myPerfomanseMetricsToDo = _settingsWorker.UpDateConfigСounterMetrics(nameСounter,doCounter);
            }
            else if (frequencyCounter>=0)
            {
                _myPerfomanseMetricsToDo = _settingsWorker.UpDateConfigСounterMetrics(nameСounter, frequencyCounter, doCounter);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Частота не может быть меньше 0");
            }
        }
    }
}
