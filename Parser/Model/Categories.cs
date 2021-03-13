using Newtonsoft.Json;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserRetail.Model
{

    public class Categories
    {
        public string Name { get; set; }
        
        [JsonProperty("results")]
        public ICollection<Product> Products { get; set; }


    }
}
