using Newtonsoft.Json;
using Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserRetail.Models
{

    public class Categories
    {
        public string Name { get; set; }
        
        [JsonProperty("results")]
        public ICollection<Product> Products { get; set; }


    }
}
