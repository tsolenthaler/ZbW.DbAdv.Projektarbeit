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
        public DbSet<ArticelGroupDTO> ArticelGroup { get; set; }
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
            customer.HasKey(x => x.Id);
            customer.Property(x => x.Firstname).IsRequired(); //Pflichtfeld
            customer.Property(x => x.Lastname).IsRequired(); //Pflichtfeld
        }
    }
}
