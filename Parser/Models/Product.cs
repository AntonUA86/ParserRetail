using Newtonsoft.Json;
using ParserRetail.Models;


namespace Parser.Models
{
    public class Product
    {
           
        public int ID { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("price")]
        public decimal price { get; set; }

        public  Categories Categories { get; set; }

      
    }
}
