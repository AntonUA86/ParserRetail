using Parser.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using View.Infrastructure.Commands;
using View.ViewModels.Base;
using Parser.Model;
using System.Linq;

namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {

        public ObservableCollection<ProductsInfo> ProductsInfo { get;}

        #region Выбранная Група Товаров

        private ProductsInfo _SelectedGroup; 

        public ProductsInfo SelectedGroup { get => _SelectedGroup; set => Set(ref _SelectedGroup, value); }


        #endregion
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


            var s_index = 1;
            var result = Enumerable.Range(1, 100).Select(i => new Result
            {
                title = $"{s_index++}",
                price = s_index++
            }
            );

            var productsInfos = Enumerable.Range(1, 50).Select(i => new ProductsInfo
            {
                Name = "Ашан",
                Categories = "Хлеб",
                Results = new ObservableCollection<Result>(result)

            });

            ProductsInfo = new ObservableCollection<ProductsInfo>(productsInfos);


        }
    }
}
