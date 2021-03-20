using Microsoft.EntityFrameworkCore;
using ParserRetail.Models;

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
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocalDB; Database=CatalogProduct ;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasOne(c => c.Stores)
                .WithMany(s => s.Categories)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Product>()
                .HasOne(c => c.Categories)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.Cascade);   
        }


     

    }
   
}
