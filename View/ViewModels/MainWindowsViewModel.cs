using Parser.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using View.Infrastructure.Commands;
using View.ViewModels.Base;


namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "Анализ цен";

        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p) 
        {
            Application.Current.Shutdown();
        }
        #endregion
        #endregion

        ControllerBase controllerBase = new ControllerBase();

        public MainWindowsViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion

      
        }
    }
}
