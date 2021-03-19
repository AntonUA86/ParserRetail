using ParserRetail.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parser.Models
{
   public class Stores
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Categories> Categories { get; set; }

    }
}
