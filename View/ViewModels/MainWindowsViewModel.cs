using Parser.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using View.Infrastructure.Commands;
using View.ViewModels.Base;
using Parser.Models;
using System.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ParserRetail.Models;


namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {
        private const string urlVarusBread = @"https://stores-api.zakaz.ua/stores/48246401/categories/bread-auchan/products/?sort=price_asc";
        private const string urlEcoMarketBread = @"https://stores-api.zakaz.ua/stores/48246401/categories/semi-hard-cheese-auchan/products/?sort=price_asc";
        private const string urlAuchanFuit = @"https://stores-api.zakaz.ua/stores/48246401/products/search/?q=%D0%A4%D1%80%D1%83%D0%BA%D1%82%D1%8B&category_id=fruits-and-vegetables-auchan";
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
        #region
      /*  public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommmandExecute(object p) => true;

        private void OnChangeTabIndexCommmandExecute(object p)
        {


            SelectedPageIndex += null ;
        }*/

        #endregion
        #endregion



        public MainWindowsViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion

            ProductInfoController productInfoController = new ProductInfoController();
            Dictionary<string,Product> product = new Dictionary<string,Product>();
            Dictionary<string, string> url = new Dictionary<string, string>();
            url.Add("Хлеб",urlVarusBread);

            /*    controllerProductInfo.SaveProductToList(product, urlVarusBread);*/
        /*    productInfoController.SaveProductToList(product, urlVarusBread,"Хлеб");*/
            productInfoController.SaveProductToList(product, urlEcoMarketBread, "Молоко");

            IEnumerable<Categories> categories = GetCategories(product);

            Categories = new ObservableCollection<Categories>(categories);


        }

        private static List<Categories> GetCategories(Dictionary<string,Product> prod)
        {
            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories { Name = prod.Keys.ToString() , Products = new ObservableCollection<Product>(prod.Values) });
            categories.Add(new Categories { Name = prod.Keys.ToString(), Products = new ObservableCollection<Product>(prod.Values) });

            return categories;
        }

        private static IEnumerable<Categories> GetCategories2(List<Product> prod, string nameCategories)
        {
            return Enumerable.Range(1, 20).Select(i => new Categories
            {
                Name = nameCategories,
                Products = new ObservableCollection<Product>(prod)
            });
        }
    }
}

