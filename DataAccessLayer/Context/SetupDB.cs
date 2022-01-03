using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class SetupDB : DbContext
    {
        public DbSet<AddressDTO> Address { get; set; }
        public DbSet<ArticleGroupDTO> ArticelGroup { get; set; }
        public DbSet<ArticleDTO> Articel { get; set; }
        public DbSet<CustomerDTO> Customer { get; set; }
        public DbSet<OrderDTO> Order { get; set; }
        public DbSet<OrderPositionDTO> OrderPosition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connection = "Server=.;Database=Auftragsverwaltung;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connection);
            }

            // install-package Microsoft.Extensions.Configuration.Json

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("Auftragsverwaltung"));

            //optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        // Spezifikationen via FluentaAPI und Create Testdaten (Seed)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var customer = modelBuilder.Entity<CustomerDTO>();
            customer.HasKey(x => x.Id); // Id hinterlegen
            customer.Property(x => x.Firstname).IsRequired(); //Pflichtfeld
            customer.Property(x => x.Lastname).IsRequired(); //Pflichtfeld
            customer.HasOne(x => x.Address); // Kunde kann nur eine Adresse haben.
                     //.WithMany(x => x.Customer); // Eine Adresse kann mehrere Kunden haben.

            var address = modelBuilder.Entity<AddressDTO>();
            address.HasKey(x => x.Id);
            address.Property(x => x.Street).IsRequired();
            address.Property(x => x.StreetNo).IsRequired();
            address.Property(x => x.City).IsRequired();
            address.Property(x => x.Plz).IsRequired();

            var article = modelBuilder.Entity<ArticleDTO>();
            article.HasKey(x => x.Id);
            article.Property(x => x.Name).IsRequired();
            article.Property(x => x.Price).IsRequired();   
            article.HasMany(x => x.ArticleGroups); // Artikel hat null oder mehrere Artikel-Gruppen

            var articlegroup = modelBuilder.Entity<ArticleGroupDTO>();
            articlegroup.HasKey(x => x.Id);
            articlegroup.Property(x => x.Name).IsRequired();

            var order = modelBuilder.Entity<OrderDTO>();
            order.HasKey(x => x.Id); // Auftragsnummer
            order.Property(x => x.Date).IsRequired();
            order.HasOne(x => x.Customer); // Eine Bestellung hat immer einen Kunden

            var orderposition = modelBuilder.Entity<OrderPositionDTO>();
            orderposition.HasKey(x => x.Id);
            orderposition.Property(x => x.Quantity).IsRequired();
            //orderposition.HasMany(x => x.Article); // Eine Position kann einen oder mehrere Artikel haben (Stimmt das?)
            //orderposition.HasMany(x => x.Order); // Eine Position kann einen oder mehrere Aufträge haben (Stimmt das? nö)
        }
    }
}
