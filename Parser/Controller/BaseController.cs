using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parser.Models;
using ParserRetail.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Controller
{
    public class BaseController : BaseContext
    {
      
 /*       public  void Save()
        {
          
            using (var client = new WebClient())
            using (var stream = client.OpenRead(data_url))
            using (var reader = new StreamReader(stream))
            {

                var jObject = JObject.Parse(reader.ReadLine());
                var feed = JsonConvert.DeserializeObject<Categories>(jObject.ToString());
               
                foreach (var item in feed.Products)
                {
                    var title = item.title;
                    var price = item.price;
                    Categories.Add(new Product { title = title, price = price });
                    SaveChanges();
                }

            }
           

            

        }*/
     
        public void Save()
        {
          
        }
    }
}

