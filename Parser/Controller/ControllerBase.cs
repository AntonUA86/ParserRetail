using HtmlAgilityPack;
using Parser.Model;
using ParserRetail.Model;
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
    public class ControllerBase : BaseContext
    {
        public  void Save(HtmlNode cssSelect)
        {
            foreach (var menu in cssSelect.SelectNodes("li"))
            {
                string titleCategories = menu.Attributes["title"]?.Value;

                Categories.Add(new Category { Name = titleCategories });
                SaveChanges();

            }

        }
    }
}

