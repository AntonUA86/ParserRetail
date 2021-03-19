using Newtonsoft.Json;
using Parser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParserRetail.Models
{

    public class Categories
    {
        
    

       
        public int ID { get; set; }

        public string Name { get; set; }
        
        [JsonProperty("results")]
        public ICollection<Product> Products { get; set; }

        public Stores Stores { get; set; }

    


    }
}
