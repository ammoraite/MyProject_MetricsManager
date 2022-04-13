using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using WPF_Client.Infrastructure.Commands;
using WPF_Client.Models.MetricModels;
using WPF_Client.ViewModels.Base;

namespace WPF_Client.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region data

        private IEnumerable<MetricValue> _metricValues;

        public IEnumerable<MetricValue> MetricValues { get=> _metricValues; set=>Set(ref _metricValues, value); }
        #endregion

        private Category _category = new Category();
        public Category _Category { get=>_category; set => Set(ref _category, value); }

        #region Заголовок окна

        private string _Title = "Клиент Сбора Метрик";

        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Status: string- Статус программы

        /// <summary> Статус программы</summary>
        private string _Status = "Готов!";

        /// <summary> Статус программы</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        #region Команды

        #region CloseAppCommand

        public ICommand CloseAppCommand { get; }

        private bool OnCloseAppCommandExecute(object p) => true;

        private void OnCloseAppCommandExecuted(object p) => Application.Current.Shutdown();


        #endregion

        #region Category

        #region Instanse

        public ICommand GetInstanse { get; }

        private bool OnGetInstanseCommandExecute(object p) => true;

        private void OnGetInstanseExecuted(object p) => _Category.GetInstanses();

        #endregion

        #region Caunter

        public ICommand GetCaunter { get; }

        private bool OnGetCaunterCommandExecute(object p) => true;

        private void OnGetCaunterExecuted(object p) => _Category.GetInstanses();

        #endregion

        #endregion

        #endregion

        public MainWindowViewModel()
        {
           
            #region Команды

            #region CloseAppCommand

            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, OnCloseAppCommandExecute);

            #endregion

            #endregion
        }

    }
}
