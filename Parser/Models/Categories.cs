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
        [ForeignKey("StoresID")]
        public int StoreID { get; set; }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        
        [JsonProperty("results")]
        public ICollection<Product> Products { get; set; }


    }
}
