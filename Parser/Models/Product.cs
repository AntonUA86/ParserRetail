using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Models
{
    public class Product 
    {
        public string Name { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("price")]
        public decimal price { get; set; }

      
    }
}
