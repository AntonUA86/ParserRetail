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
        /*private const string data_url = @"https://stores-api.zakaz.ua/stores/48246401/products/search/?q=%D0%A5%D0%BB%D0%B5%D0%B1&per_page=100";*/
        public ObservableCollection<Categories> Categories { get; set; }

        #region Выбор Товара

        private string _SelectedCategories = "Категория";

        public string SelectedCategories { get => _SelectedCategories; set => Set(ref _SelectedCategories, value); }


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

            var ProducttIndex = 1;
            var products = Enumerable.Range(1, 10).Select(i => new Product
            {
               
                title = $"title {ProducttIndex++}",
                 Name = $"name {ProducttIndex++}"
            }
            );

            var categories = Enumerable.Range(1, 20).Select(i => new Categories
            {
                Name = "Хлеб",
                Products = new ObservableCollection<Product>(products)
            });

            Categories = new ObservableCollection<Categories>(categories);


        }

        /*private static (string title, int price)  GoToPrice()
        {

            
            using (var client = new WebClient())
            using (var stream = client.OpenRead(data_url))
            using (var reader = new StreamReader(stream))
            {
              
                var jObject = JObject.Parse(reader.ReadLine());
                var feed = JsonConvert.DeserializeObject<Categories>(jObject.ToString());

                foreach (var item in feed.Product)
                {
                    
                    var result = (title: item.title, price: item.price);
                    return result;
                }
                return (null,33);
            }




        }*/
    }
}
