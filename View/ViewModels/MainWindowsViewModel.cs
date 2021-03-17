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
using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows.Data;
using System.ComponentModel;

namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {

        #region URLCategories
        private const string urlAuchanBread = @"https://stores-api.zakaz.ua/stores/48246401/categories/bread-auchan/products/?sort=price_asc";
        private const string urlAuchanBagels = @"https://stores-api.zakaz.ua/stores/48246401/categories/bagels-auchan/products/?sort=price_asc";
        private const string urlAuchandriedCrust = @"https://stores-api.zakaz.ua/stores/48246401/categories/dried-crust-and-rolls-auchan/products/?sort=price_asc";
        private const string urlAuchanWheatBread = @"https://stores-api.zakaz.ua/stores/48246401/categories/wheat-bread-and-shortcake-auchan/products/?sort=price_asc";
        private const string urlAuchanCakesAndPies = @"https://stores-api.zakaz.ua/stores/48246401/categories/cakes-and-pies-auchan/products/?sort=price_asc";



        private const string urlNovusBread = @"https://stores-api.zakaz.ua/stores/48201070/categories/bread/products/?sort=price_asc";
        private const string urlNovusBagels = @"https://stores-api.zakaz.ua/stores/48201070/categories/bagels/products/?sort=price_asc";
        private const string urlNovusDriedCrust = @"https://stores-api.zakaz.ua/stores/48201070/categories/dried-crust-and-rolls/products/?sort=price_asc";
        private const string urlNovusWheatBread = @"https://stores-api.zakaz.ua/stores/48201070/categories/wheat-bread-and-shortcake/products/?sort=price_asc";

        private const string urlEcoMarketBread = @"https://stores-api.zakaz.ua/stores/48280214/categories/bread-ekomarket/products/?sort=price_asc";
        private const string urlEcoMarketBagels = @"https://stores-api.zakaz.ua/stores/48280214/categories/bagels-ekomarket/products/?sort=price_asc";
        private const string urlEcoMarketDriedCrust = @"https://stores-api.zakaz.ua/stores/48280214/categories/dried-crust-and-rolls-ekomarket/products/?sort=price_asc";
        private const string urlEcoMarketLavash = @"https://stores-api.zakaz.ua/stores/48280214/categories/lavash-ekomarket/products/?sort=price_asc";

        private const string urlVarusBread = @"https://stores-api.zakaz.ua/stores/48241001/categories/bread-varus/products/?sort=price_asc";
        private const string urlVarusBagels = @"https://stores-api.zakaz.ua/stores/48241001/categories/bagels-varus/products/?sort=price_asc";
        private const string urlVarusLavash = @"https://stores-api.zakaz.ua/stores/48241001/categories/lavash-varus/products/?sort=price_asc";
        #endregion



        public ObservableCollection<Categories> CategoriesAuchan { get; }
        public ObservableCollection<Categories> CategoriesNovus { get; }
        public ObservableCollection<Categories> CategoriesEcoMarket { get; }
        public ObservableCollection<Categories> CategoriesVarus { get; }
        public static ProductInfoController productInfoController { get; set; }

        #region Текст фильтра продуктов
        private string _ProductFilterText;

        public string ProductFilterText
        {
            get => _ProductFilterText;
            set
            {
                if (!Set(ref _ProductFilterText, value)) return;
                _SelectedCategoriesProduct.View.Refresh();
            }
        }
        #endregion

        #region Выбор Категории

        private Categories _SelectedCategories;

        public Categories SelectedCategories
        {
            get => _SelectedCategories;
            set
            {
                if (!Set(ref _SelectedCategories, value)) return;

                _SelectedCategoriesProduct.Source = value?.Products;
                OnPropertyChanged(nameof(SelectedCategoriesProduct));
            }
        }



        #endregion


        #region Заголовок окна
        private string _Title = "Анализ цен";

        public string Title { get => _Title; set => Set(ref _Title, value); }




        #endregion

        #region SelectedCategoriesProduct Filtred
        private readonly CollectionViewSource _SelectedCategoriesProduct = new CollectionViewSource();

        public ICollectionView SelectedCategoriesProduct => _SelectedCategoriesProduct?.View;

        private void OnProductFiltred(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product)  )
            {
                e.Accepted = false;
                return;
            }

            var filterText = ProductFilterText;
            if (string.IsNullOrWhiteSpace(filterText))
                return;

            if (product.Name is null)
            {
                e.Accepted = false;
                return;
            }

            if (product.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;

        }
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


            productInfoController = new ProductInfoController();




            CategoriesVarus = new ObservableCollection<Categories>(GetCategoriesVarus());
            CategoriesEcoMarket = new ObservableCollection<Categories>(GetCategoriesEcoMarket());
            CategoriesNovus = new ObservableCollection<Categories>(GetCategoriesNovus());
            CategoriesAuchan = new ObservableCollection<Categories>(GetCategoriesAuchan());

            _SelectedCategoriesProduct.Filter += OnProductFiltred;
        }

      


        #region CreateObservableCollectionCategories
        private static List<Categories> GetCategoriesAuchan()
        {
            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories { Name = "Хлеб", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlAuchanBread)) });
            categories.Add(new Categories { Name = "Булочки", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlAuchanBagels)) });
            categories.Add(new Categories { Name = "Сушки и сухари", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlAuchandriedCrust)) });
            categories.Add(new Categories { Name = "Лаваши и коржи", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlAuchanWheatBread)) });
            categories.Add(new Categories { Name = "Торты и пирожные", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlAuchanCakesAndPies)) });

            return categories;
        }

        private static List<Categories> GetCategoriesNovus()
        {
            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories { Name = "Хлеб", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlNovusBread)) });
            categories.Add(new Categories { Name = "Булочки", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlNovusBagels)) });
            categories.Add(new Categories { Name = "Сушки и сухари", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlNovusDriedCrust)) });
            categories.Add(new Categories { Name = "Лаваши и коржи", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlNovusWheatBread)) });


            return categories;
        }
        private static List<Categories> GetCategoriesEcoMarket()
        {
            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories { Name = "Хлеб", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlEcoMarketBread)) });
            categories.Add(new Categories { Name = "Булочки", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlEcoMarketBagels)) });
            categories.Add(new Categories { Name = "Сушки и сухари", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlEcoMarketDriedCrust)) });
            categories.Add(new Categories { Name = "Лаваш", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlEcoMarketLavash)) });
            return categories;
        }
        private static List<Categories> GetCategoriesVarus()
        {
            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories { Name = "Хлеб", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlVarusBread)) });
            categories.Add(new Categories { Name = "Булочки", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlVarusBagels)) });
            categories.Add(new Categories { Name = "Лаваш", Products = new ObservableCollection<Product>(productInfoController.SaveProductToList(urlVarusLavash)) });
            return categories;
        }
        #endregion

    }
}
