using Newtonsoft.Json;
using ParserRetail.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parser.Models
{
    public class Product
    {
      
        public int CategorieID { get; set; }

       
        public int ID { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("price")]
        public decimal price { get; set; }




    }
}
