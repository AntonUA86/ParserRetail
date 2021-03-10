using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Model
{
    public class Result
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }
    }
}
