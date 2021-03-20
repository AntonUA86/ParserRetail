using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parser.Models;
using ParserRetail.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parser.Controller
{
    public class ProductInfoController
    {
        public static string urlAuchanBread = @"https://stores-api.zakaz.ua/stores/48246401/categories/";
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

        public static List<string> GetStoreCategories()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.OpenRead(urlAuchanBread);
                using (var reader = new StreamReader(response))
                {
                    var categories = new List<string>();
                    var jArray = JArray.Parse(reader.ReadToEnd());
                    foreach (var category in jArray)
                    {
                        AddCategory(category);
                    }
                    void AddCategory(JToken category)
                    {
                        categories.Add(category["id"].Value<string>());
                        var children = category["children"];

                        if (children.HasValues)
                        {
                            foreach (var child in children)
                            {

                                AddCategory(child);
                            }
                        }
                    }
                    return categories;
                }

            }



        }

        public async Task<SortedList<string, decimal>> GetData()
        {
            using (var client = new HttpClient())
            {
                var Products = new Product();
                var Categories = new Categories();
                SortedList<string, decimal> ProductsInfo = new SortedList<string, decimal>();
                var responseCategories = GetStoreCategories();
                foreach (var category in responseCategories)
                {
                    HttpResponseMessage response = await client.GetAsync($"https://stores-api.zakaz.ua/stores/48246401/categories/{category}/products");
                    var results = JObject.Parse(await
                    response.Content.ReadAsStringAsync())["results"];
                    foreach (var GetInfo in results)
                    {
                        ProductsInfo.Add(Products.title = GetInfo["title"].Value<string>(), Products.price = GetInfo["price"].Value<decimal>() / 100);
                    }
                    
                }

                return ProductsInfo;


            }
        }
    }
}


