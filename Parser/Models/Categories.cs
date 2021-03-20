using Newtonsoft.Json;
using Parser.Models;
using System.Collections.Generic;


namespace ParserRetail.Models
{

    public class Categories
    {
        public int Id { get; set; }
    
        public string Name { get; set; }
        
        [JsonProperty("results")]
        public ICollection<Product> Products { get; set; }
   
        public Stores Stores { get; set; }

    


    }
}
