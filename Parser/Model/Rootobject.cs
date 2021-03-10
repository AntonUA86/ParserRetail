using Newtonsoft.Json;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserRetail.Model
{ /// <summary>
  /// Products
  /// </summary>
    public class Rootobject
    {
        [JsonProperty("results")]
        public Result[] results { get; set; }
     
    }
}
