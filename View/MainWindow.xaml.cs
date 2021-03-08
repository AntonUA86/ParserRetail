using HtmlAgilityPack;
using Parser.Controller;
using Parser.Model;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;



namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ControllerBase controllerBase = new ControllerBase();
        Browser browser = new Browser();
        

        public MainWindow()
        {
            
            InitializeComponent();
       
            
        }

        private void ActiveFilter(object sender, RoutedEventArgs e)
        {

       
        
        }



        private  void Dealer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxDealer.SelectedIndex == 0)
            {
                controllerBase.Save(browser.Link());
         

            }


        }




        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    

    }
}
