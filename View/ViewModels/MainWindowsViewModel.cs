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
using Microsoft.EntityFrameworkCore;

namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {






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
                _SelectedCategoriesProducts.View.Refresh();
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

                _SelectedCategoriesProducts.Source = value?.Products;
                OnPropertyChanged(nameof(SelectedCategoriesProducts));
            }
        }



        #endregion

        #region DataPoint
        private IEnumerable<DataPoint> _DataPoints;

        public IEnumerable<DataPoint> DataPoints { get => _DataPoints; set => Set(ref _DataPoints, value); }

        #endregion


        #region Заголовок окна
        private string _Title = "Анализ цен";
        public string Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region SelectedCategoriesProduct Filter
        private readonly CollectionViewSource _SelectedCategoriesProducts = new CollectionViewSource();

        public ICollectionView SelectedCategoriesProducts => _SelectedCategoriesProducts?.View;

        private void OnProductFiltred(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Product product))
            {
                e.Accepted = false;
                return;
            }

            var filterText = ProductFilterText;
            if (string.IsNullOrWhiteSpace(filterText))
                return;

            if (product.title is null)
            {
                e.Accepted = false;
                return;
            }

            if (product.title.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

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
        #region SaveDBCommand
        public ICommand SaveDBCommand { get; }
        private bool CanSaveDBCommandCommandExecute(object p) => true;
        private void OnSaveDBCommandCommandExecuted(object p)
        {
            using (BaseContext Base = new BaseContext())
            {
                if (Base.Database.CanConnect() == false)
                    Base.Database.EnsureCreated();


                foreach (var item in Base.Stores)
                    if (item != null)
                        Base.Stores.Remove(item);


                Base.SaveChanges();







                Stores Auchan = new Stores { Name = "Auchan", Categories = CategoriesAuchan.ToList() };
                Stores Novus = new Stores { Name = "Novus", Categories = CategoriesNovus.ToList() };
                Stores EcoMarket = new Stores { Name = "EcoMarket", Categories = CategoriesEcoMarket.ToList() };
                Stores Varus = new Stores { Name = "Varus", Categories = CategoriesVarus.ToList() };
                Base.Stores.AddRange(Auchan, Novus, EcoMarket, Varus);


                Base.SaveChanges();




            }
        }

        #endregion
        #endregion




        public MainWindowsViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SaveDBCommand = new LambdaCommand(OnSaveDBCommandCommandExecuted, CanSaveDBCommandCommandExecute);
            #endregion

            var data_point = new List<DataPoint>((int)(360 / 0.1));
            for (double x = 0d; x <= 360; x += 0.1)
            {
                const double rad = Math.PI / 180;
                double y = Math.Sin(x * rad);

                data_point.Add(new DataPoint { XValue = x, YValue = y });
            }

            DataPoints = data_point;

            productInfoController = new ProductInfoController();




            CategoriesVarus = new ObservableCollection<Categories>(GetCategoriesVarus());
            CategoriesEcoMarket = new ObservableCollection<Categories>(GetCategoriesEcoMarket());
            CategoriesNovus = new ObservableCollection<Categories>(GetCategoriesNovus());
            CategoriesAuchan = new ObservableCollection<Categories>(GetCategoriesAuchan());

            _SelectedCategoriesProducts.Filter += OnProductFiltred;

        }






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