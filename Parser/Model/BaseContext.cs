using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParserRetail.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Parser.Model
{
      public class BaseContext : DbContext
        {
        
/*        public DbSet<Rootobject> Products { get; set; }*/
        public DbSet<Result> Categories { get; set; }


        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=Products.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
   
}
