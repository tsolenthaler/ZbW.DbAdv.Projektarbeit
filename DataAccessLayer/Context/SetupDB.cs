using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    internal class SetupDB : DbContext
    {
        public DbSet<AddressDTO> Address { get; set; }
        public DbSet<ArticelGroupDTO> ArticelGroup { get; set; }
        public DbSet<ArticleDTO> Articel { get; set; }
        public DbSet<CustomerDTO> Customer { get; set; }
        public DbSet<OrderDTO> Order { get; set; }
        public DbSet<OrderPositionDTO> OrderPosition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Auftragsverwaltung;Trusted_Connection=True;");

            optionsBuilder.UseLazyLoadingProxies();

            // install-package Microsoft.Extensions.Configuration.Json

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("Auftragsverwaltung"));

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}
