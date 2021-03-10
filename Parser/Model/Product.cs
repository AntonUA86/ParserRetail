using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Model
{
    public class Product 
    {
        public string Name { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }

      
    }
}
