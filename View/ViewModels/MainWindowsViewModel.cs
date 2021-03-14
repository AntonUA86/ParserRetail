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
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ParserRetail.Model;


namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {
        private const string url = @"https://stores-api.zakaz.ua/stores/48246401/products/search/?q=%D0%A5%D0%BB%D0%B5%D0%B1&per_page=100";
        public ObservableCollection<Categories> Categories { get; }

        #region Выбор Категории

        private Categories _SelectedCategories;

        public Categories SelectedCategories
        {
            get => _SelectedCategories;
            set => Set(ref _SelectedCategories, value);
        }


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



        public MainWindowsViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion

            ControllerProductInfo controllerProductInfo = new ControllerProductInfo();
            List<Product> product = new List<Product>();



            controllerProductInfo.SaveProductToList(product, url);

            IEnumerable<Categories> categories = GetCategories(product,"Хлеб");

            Categories = new ObservableCollection<Categories>(categories);


        }

        private static IEnumerable<Categories> GetCategories(List<Product> prod, string nameCategories)
        {
            return Enumerable.Range(1, 20).Select(i => new Categories
            {
                Name = nameCategories,
                Products = new ObservableCollection<Product>(prod)
            });
        }
    }
}

