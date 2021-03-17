using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parser.Models;
using ParserRetail.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Parser.Controller
{
    public class ProductInfoController
    {
        private static SortedList<string, decimal> GoToInfoProduct(string url)
        {
            using (var client = new WebClient())                                   
            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                  
                SortedList<string, decimal> ProductsInfo = new SortedList<string, decimal>();
                var jObject = JObject.Parse(reader.ReadLine());
                var feed = JsonConvert.DeserializeObject<Categories>(jObject.ToString());

                foreach (var item in feed.Products)
                    ProductsInfo.Add(item.title, item.price / 100);
                return ProductsInfo;
                
            }
        }

        public List<Product> SaveProductToList(string url)
        {
            List<Product> products = new List<Product>();
            foreach (var p in GoToInfoProduct(url))
                products.Add(new Product { title = p.Key, price = p.Value });
            return products;

        }


    }
}
