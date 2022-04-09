using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace MyMetricsMeneger
{
    public class SettingsWorker
    {
        private List<MyPerfomanseCategory> _myDefaultPerfomanseCategory;

        private ItemForSaveLoadConfig _itemForSaveLoadConfig;
        public string ConfigFilePath { get; private set; }
        public SettingsWorker(string configFilePath)
        {
            ConfigFilePath = configFilePath;

            if (CheckNewPathAndSetinWorker(ConfigFilePath))
            {
                LoadSettings(ConfigFilePath);
            }
            else
            {
                InitSettings();
            }

        }
        /// <summary>
        /// Инициализация настроек
        /// </summary>
        private void InitSettings()
        {
            File.Create(ConfigFilePath);
            _myDefaultPerfomanseCategory = GetDefaultDictionaryFarmerConfigs();
            _itemForSaveLoadConfig = new();
            _itemForSaveLoadConfig._configForSave = _myDefaultPerfomanseCategory;
        }

        /// <summary>
        /// Загрузка настроек
        /// </summary>
        private void LoadSettings(string configFilePath)
        {
            string json = File.ReadAllText(configFilePath);

            _itemForSaveLoadConfig = JsonSerializer.Deserialize<ItemForSaveLoadConfig>(json);

            if (_myDefaultPerfomanseCategory == null)
            {
                _myDefaultPerfomanseCategory = GetDefaultDictionaryFarmerConfigs();
            }
            if (_myDefaultPerfomanseCategory.Count != _itemForSaveLoadConfig._configForSave.Count)
            {
                _itemForSaveLoadConfig._configForSave = _myDefaultPerfomanseCategory;
            }
        }

        /// <summary>
        /// Проверяет возможность изменить путь файла настроек и устанавливает его для воркера
        /// </summary>
        public bool CheckNewPathAndSetinWorker(string value)
        {
            bool a=false;
            if (string.IsNullOrEmpty(value)&&File.Exists(value))
            {
                a = true;
                ConfigFilePath = value;
            }
            return a;
        }


        /// <summary>Задает начальный словарь с настройками для SettingsWorker. Поумолчанию все значения словаря = false</summary>
        /// <returns></returns>
        private List<MyPerfomanseCategory> GetDefaultDictionaryFarmerConfigs()
        {
            List<MyPerfomanseCategory> myPerfomanseCategoryes = new List<MyPerfomanseCategory>();

            foreach (var _itemcategory in PerformanceCounterCategory.GetCategories())
            {
                MyPerfomanseCategory myPerfomanseCategory = new MyPerfomanseCategory() 
                { _enabled=false,_categoryName=_itemcategory.CategoryName};

                if ((!_itemcategory.CategoryName.Contains("Процесс") &&
                     !_itemcategory.CategoryName.Contains("Поток")) ||
                      _itemcategory.CategoryName.Contains("Процессор"))
                {
                    myPerfomanseCategory._enabled = true;
                    foreach (var itemInstanseName in _itemcategory.GetInstanceNames())
                    {
                        try
                        {
                            if (_itemcategory.InstanceExists(itemInstanseName))
                            {
                                foreach (var itemCounter in _itemcategory.GetCounters(itemInstanseName))
                                {
                                    MyPerfomanseCounter _myPerfomanseCounter= new MyPerfomanseCounter() 
                                    { _enabled=false,_performanceCounter=itemCounter,_frequencyCounter=5};
                                    if (itemCounter != null&& !myPerfomanseCategory._performanceCounters.Contains(_myPerfomanseCounter))
                                    {
                                        if (_myPerfomanseCounter._performanceCounter.CounterName=="% загруженности процессора")
                                        {
                                            _myPerfomanseCounter._enabled = true;
                                            myPerfomanseCategory._performanceCounters.Add(_myPerfomanseCounter);
                                        }
                                        
                                    }
                                }
                            }

                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                }
                
                    if (myPerfomanseCategory._performanceCounters.Count!=0)
                    {
                        myPerfomanseCategoryes.Add(myPerfomanseCategory);
                    }
            }
            return myPerfomanseCategoryes;
        }

        /// <summary>
        /// Включает или выключает Сounter(содержит параметры для сбора метрик),устанавливает частоту сбора метрики 
        /// </summary>
        /// <param name="nameСounter"></param>
        /// <param name="_frequencyCounter"></param>
        /// <param name="doCounter"></param>
        /// <returns></returns>
        public List<MyPerfomanseCategory> UpDateConfigСounterMetrics(string nameСounter,int _frequencyCounter, bool doCounter)
        {
            foreach (var _itemMyPerfomanseCategory in _myDefaultPerfomanseCategory)
            {
                foreach (var _itemCounter in _itemMyPerfomanseCategory._performanceCounters)
                {
                    if (_itemCounter._performanceCounter.CounterName== nameСounter)
                    {
                        _itemCounter._enabled = doCounter;
                        _itemCounter._frequencyCounter = _frequencyCounter;
                    }
                }
            }
            return GetListFarmerConfigs();
        }

        /// <summary>
        /// Включает или выключает Сounter(содержит параметры для сбора метрик)
        /// </summary>
        /// <param name="nameСounter"></param>
        /// <param name="_frequencyCounter"></param>
        /// <param name="doCounter"></param>
        /// <returns></returns>
        public List<MyPerfomanseCategory> UpDateConfigСounterMetrics(string nameСounter, bool doCounter)
        {
            foreach (var _itemMyPerfomanseCategory in _myDefaultPerfomanseCategory)
            {
                foreach (var _itemCounter in _itemMyPerfomanseCategory._performanceCounters)
                {
                    if (_itemCounter._performanceCounter.CounterName == nameСounter)
                    {
                        _itemCounter._enabled = doCounter;
                    }
                }
            }
            return GetListFarmerConfigs();
        }

        /// <summary>
        /// Устанавливает категорию метрик в рабочее состояние
        /// </summary>
        /// <param name="nameCategory"></param>
        /// <param name="doCategory"></param>
        /// <returns></returns>
        public List<MyPerfomanseCategory> UpDateConfigCategoryMetrics(string nameCategory, bool doCategory)
        {
            foreach (var _itemMyPerfomanseCategory in _myDefaultPerfomanseCategory)
            {
                if (_itemMyPerfomanseCategory._categoryName== nameCategory)
                {
                    _itemMyPerfomanseCategory._enabled = doCategory;
                }
            }
            return GetListFarmerConfigs();
        }

        /// <summary>
        /// Возвращает лист с метриками которые нужно собирать
        /// </summary>
        /// <returns></returns>
        public List<MyPerfomanseCategory> GetListFarmerConfigs()
        {
            List<MyPerfomanseCategory> listMyPerfomanseCategories = new List<MyPerfomanseCategory>();
            foreach (var _itemMyPerfomanseCategory in _myDefaultPerfomanseCategory)
            {
                if (_itemMyPerfomanseCategory._enabled)
                {
                    foreach (var _itemCounters in _itemMyPerfomanseCategory._performanceCounters)
                    {
                        if (_itemCounters._enabled&& !listMyPerfomanseCategories.Contains(_itemMyPerfomanseCategory))
                        { 
                                listMyPerfomanseCategories.Add(_itemMyPerfomanseCategory);  
                        }
                    }
                }
            }
            return listMyPerfomanseCategories;
        }
    }
    [Serializable]
    public class ItemForSaveLoadConfig
    {
       public List<MyPerfomanseCategory> _configForSave { get; set; }
    }
}