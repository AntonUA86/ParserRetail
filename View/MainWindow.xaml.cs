using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Parser.Controller;
using Parser.Model;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ControllerBase _controllerBase = new ControllerBase();
        private CollectionViewSource categoryViewSource;
        private readonly Browser _browser = new Browser();
        Thread The;
        List<string> Products = new List<string>();



        public MainWindow()
        {
            InitializeComponent();

            categoryViewSource = (CollectionViewSource)FindResource(nameof(categoryViewSource));

            The = new Thread(go) { IsBackground = true };
            The.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
           

            // загружаем сущности в EF Core
            /*     _controllerBase.Categories.Load();*/

            // привязать к источнику
            categoryViewSource.Source =
               _controllerBase.Categories.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*_controllerBase.Database.EnsureCreated();*/
           

            // это заставляет сетку обновляться до последних значений
            /*   categoryDataGrid.Items.Refresh();
               productsDataGrid.Items.Refresh();*/
        }

       private  void go()
        {
            foreach (var prod in _browser.Link().InnerText)
            {
                Products.Add(prod.ToString());
            }
          
         /*   _controllerBase.Save(_browser.Link());*/

        }


      

    }
}
