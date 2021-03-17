using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParserRetail.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Parser.Models
{
      public class BaseContext : DbContext
        {
        

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Stores> Stores { get; set; }


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
