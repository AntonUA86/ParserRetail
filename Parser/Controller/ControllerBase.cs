using HtmlAgilityPack;
using Parser.Model;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Controller
{
    public class ControllerBase
    {

        public void Save(HtmlNode cssSelect)
        {
            using (BaseContext db = new BaseContext())
            {

                foreach (var menu in cssSelect.SelectNodes("li"))
                {
                    string titleCategories = menu.Attributes["title"]?.Value;

                    db.ProductsTitle.Add(new CategoriesMenu { Сategory = titleCategories });
                    db.SaveChanges();


                }

            }
        }

        public void  Load()
        {
            using (BaseContext db = new BaseContext())
            {
                var products = db.ProductsTitle.ToList();
             
    
            }
             

        }


    }
}
