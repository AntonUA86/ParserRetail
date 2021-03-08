using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Model
{
    public class BaseContext : DbContext
    {
        public DbSet<CategoriesMenu> ProductsTitle { get; set; }
        public DbSet<CategoriesSubMenu> CategoriesSubMenus { get; set; }
        public DbSet<ProductInfo> ProductInf { get; set; }

        public BaseContext() 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductsList;Trusted_Connection=True;");
        }
    }
}
