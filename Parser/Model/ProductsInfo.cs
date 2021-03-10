using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Model
{
    public class ProductsInfo
    {
        public string Name { get; set; }
        public string Categories { get; set; }

        public ICollection<Result> Results { get; set; }

    }
}
