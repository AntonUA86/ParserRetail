using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parser.Model;
using ParserRetail.Model;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Controller
{
    public class ControllerBase : BaseContext
    {
        private const string data_url = @"https://stores-api.zakaz.ua/stores/48246401/products/search/?q=%D0%A5%D0%BB%D0%B5%D0%B1&per_page=100";
        public  void Save()
        {
          
            using (var client = new WebClient())
            using (var stream = client.OpenRead(data_url))
            using (var reader = new StreamReader(stream))
            {

                var jObject = JObject.Parse(reader.ReadLine());
                var feed = JsonConvert.DeserializeObject<Rootobject>(jObject.ToString());
                for (int i = 0; i < feed.results.Length; i++)
                {
                    var title = feed.results[i].title;
                    var price = feed.results[i].price;
                    Categories.Add(new Result { title = title, price = price});
                    SaveChanges();

                }


            }
           

            

        }
    }
}

