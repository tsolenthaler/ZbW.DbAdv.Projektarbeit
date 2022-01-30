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
        public DbSet<AddressDTO> Addresses { get; set; }
        public DbSet<ArticleGroupDTO> ArticelGroups { get; set; }
        public DbSet<ArticleDTO> Articles { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<OrderPositionDTO> OrderPositions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string Thomas: "Server=.;Database=Auftragsverwaltung;Trusted_Connection=True;"
                //string Angelo: @"Server=KOLLEG-MPC\ZBW;Database=Auftragsverwaltung;Trusted_Connection=True;"
                //string Corina:
                string connection = @"Server=KOLLEG-MPC\ZBW;Database=Auftragsverwaltung;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connection);
                optionsBuilder.LogTo(Console.WriteLine);
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
            customer.Property(x => x.Firstname).IsRequired(true); //Pflichtfeld
            customer.Property(x => x.Lastname).IsRequired(true); //Pflichtfeld
            customer.Property(x => x.EMail).IsRequired(false); 
            customer.Property(x => x.Website).IsRequired(false); 
            customer.Property(x => x.Password).IsRequired(false); 
            customer.HasOne(x => x.Address); // Kunde kann nur eine Adresse haben.
                     //.WithMany(x => x.Customer); // Eine Adresse kann mehrere Kunden haben.

            var address = modelBuilder.Entity<AddressDTO>();
            address.HasKey(x => x.Id);
            address.Property(x => x.Street).IsRequired(true);
            address.Property(x => x.StreetNo).IsRequired(false);
            address.Property(x => x.City).IsRequired(false);
            address.Property(x => x.Plz).IsRequired(false);

            var article = modelBuilder.Entity<ArticleDTO>();
            article.HasKey(x => x.Id);
            article.Property(x => x.Name).IsRequired(true);
            article.Property(x => x.Price).IsRequired(true);   
            article.HasOne(x => x.ArticleGroup); // Artikel hat eine Artikelgruppe

            var articlegroup = modelBuilder.Entity<ArticleGroupDTO>();
            articlegroup.HasKey(x => x.Id);
            articlegroup.Property(x => x.Name).IsRequired(true);
            articlegroup.Property(x => x.ParentArticleGroupId).IsRequired(true);

            var order = modelBuilder.Entity<OrderDTO>();
            order.HasKey(x => x.Id); // Auftragsnummer
            order.Property(x => x.Date).IsRequired(true);
            order.HasOne(x => x.Customer); // Eine Bestellung hat immer einen Kunden

            var orderposition = modelBuilder.Entity<OrderPositionDTO>();
            orderposition.HasKey(x => x.Id);
            orderposition.Property(x => x.Quantity).IsRequired();
            orderposition.HasOne(x=>x.Article); // Eine Position hat nur einen Artikel

            //Add Testdata
            //customer.HasData(new CustomerDTO()
            //{
            //    //Id = 3,
            //    Firstname = "Hans",
            //    Lastname = "Muster",
            //    Address = new AddressDTO()
            //    {
            //        //Id = 5,
            //        Street = "Musterstrasse"
            //    }
            //});
        }
    }
}
