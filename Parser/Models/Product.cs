using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parser.Models
{
    public class Product
    {
        [ForeignKey("CategorieID")]
        public int CategorieID { get; set; }

        [Key]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("price")]
        public decimal price { get; set; }

      
    }
}
