using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parser.Models
{
   public class Stores
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
    

    }
}
