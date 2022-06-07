using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDanych
{
    public class DrinkiContext : DbContext
    {
        public string DbPath { get; }

        public DbSet<Skladnik> Skladniki { get; set; }
        public DbSet<DrinkSkladnik> DrinkiSkladniki { get; set; }
        public DbSet<Drink> Drinki { get; set; }

        public DrinkiContext()
        {
            var folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); 
            DbPath = System.IO.Path.Join(folder, "MojeDrinki.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //if (options.IsConfigured)
            options.UseSqlite($"Data Source={DbPath}");

            options.EnableSensitiveDataLogging(true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skladnik>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Skladnik>()
                .Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Drink>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Drink>()
                .Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<DrinkSkladnik>().
                HasKey(p => new {p.IdDrinka, p.IdSkladnika});

            // Updating many to many relationships in Entity Framework Core
            // https://www.thereformedprogrammer.net/updating-many-to-many-relationships-in-entity-framework-core/ 

            modelBuilder.Entity<DrinkSkladnik>()
                 .HasOne(pt => pt.Drink)
                 .WithMany(p => p.DrinkSkladniki)
                 .HasForeignKey(pt => pt.IdDrinka);

            modelBuilder.Entity<DrinkSkladnik>()
                .HasOne(pt => pt.Skladnik)
                .WithMany(t => t.DrinkSkladniki)
                .HasForeignKey(pt => pt.IdSkladnika);
        }
    }
    
    public class Skladnik
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }        

        public ICollection<DrinkSkladnik> DrinkSkladniki { get; set; }
    }

    // many - to - many
    public class DrinkSkladnik
    {
        public int IdDrinka { get; set; }
        public int IdSkladnika { get; set; }
        public string? Miara { get; set; }

        public Skladnik Skladnik { get; set; }
        public Drink Drink { get; set; }
    }

    public class Drink
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        
        public string Przepis { get; set; }
        
        // identyfikator drinka w serwisie TheCoctails..
        public string? IdDrinkDB { get; set; }
        
        public string? Tagi { get; set; }
        public string? Kategoria { get; set; }
        
        public string? Miniatura { get; set; }
        public string? Grafika { get; set; }

        public ICollection<DrinkSkladnik> DrinkSkladniki { get; set; }
    }
}
