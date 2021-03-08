using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Controller
{
  public  class Browser 
    {

        ScrapingBrowser browser = new ScrapingBrowser();
         
        public  HtmlNode Link()
        {
            /// <summary>
            /// Загрузить веб-страницу
            /// </summary>       
            WebPage LinkMenuVarus = browser.NavigateToPage(new Uri("https://varus.zakaz.ua/ru/"));
            /// <summary>
            /// все элементы списка, содержащие .product-tile__prices
            /// </summary>
            HtmlNode CssSelectMenuVarus =  LinkMenuVarus.Html.CssSelect(".CategoriesMenu__list").Single();
            return  CssSelectMenuVarus;
        }
    }
}
