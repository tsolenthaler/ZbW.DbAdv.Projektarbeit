﻿using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Customer;
using DataAccessLayer.Invoice;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;
using Microsoft.EntityFrameworkCore;

//using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Context
{
    public class SetupDB : DbContext
    {
        public virtual DbSet<AddressDTO> Addresses { get; set; }
        public virtual DbSet<ArticleGroupDTO> ArticelGroups { get; set; }
        public virtual DbSet<ArticleDTO> Articles { get; set; }
        public virtual DbSet<CustomerDTO> Customers { get; set; }
        public virtual DbSet<OrderDTO> Orders { get; set; }
        public virtual DbSet<OrderPositionDTO> OrderPositions { get; set; }
        public virtual DbSet<InvoiceDTO> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string Thomas: @"Server=.;Database=Auftragsverwaltung;Trusted_Connection=True;";
                //string Angelo: @"Server=KOLLEG-MPC\ZBW;Database=Auftragsverwaltung;Trusted_Connection=True;";
                //string Corina: @"Server=.;Database=AuftragsverwaltungHistory;Trusted_Connection=True;";

                //DATABASE MUSS ZWINGEND - AuftragsverwaltungHistory - HEISSEN!
                string connection = @"Server=.;Database=AuftragsverwaltungHistory;Trusted_Connection=True;";

                optionsBuilder.UseSqlServer(connection);
                optionsBuilder.LogTo(Console.WriteLine);
            }

        }

        // Spezifikationen via FluentAPI und Create Testdaten (Seed)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var customer = modelBuilder.Entity<CustomerDTO>();
            customer.ToTable("Customer", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            customer.HasKey(x => x.Id); // Id hinterlegen
            customer.Property(x => x.Company).IsRequired(true);
            customer.Property(x => x.Firstname).IsRequired(true); //Pflichtfeld
            customer.Property(x => x.Lastname).IsRequired(true); //Pflichtfeld
            customer.Property(x => x.EMail).IsRequired(false); 
            customer.Property(x => x.Website).IsRequired(false); 
            customer.Property(x => x.Password).IsRequired(false); 
            customer.HasOne(x => x.Address); // Kunde kann nur eine Adresse haben.
            //customer.HasOne(x => x.Order); // Hat keine oder mehrere Bestellungen
                     //.WithMany(x => x.Customer); // Eine Adresse kann mehrere Kunden haben.

            var address = modelBuilder.Entity<AddressDTO>();
            address.ToTable("Addresses", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            address.HasKey(x => x.Id);
            address.Property(x => x.Street).IsRequired(true);
            address.Property(x => x.StreetNo).IsRequired(false);
            address.Property(x => x.City).IsRequired(false);
            address.Property(x => x.Plz).IsRequired(false);

            var article = modelBuilder.Entity<ArticleDTO>();
            article.ToTable("Articles", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            article.HasKey(x => x.Id);
            article.Property(x => x.Name).IsRequired(true);
            article.Property(x => x.Price).IsRequired(true);   
            article.HasOne(x => x.ArticleGroup); // Artikel hat eine Artikelgruppe

            var articlegroup = modelBuilder.Entity<ArticleGroupDTO>();
            articlegroup.ToTable("ArticelGroups", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            articlegroup.HasKey(x => x.Id);
            articlegroup.Property(x => x.Name).IsRequired(true);
            articlegroup.Property(x => x.ParentArticleGroupId).IsRequired(true);

            var order = modelBuilder.Entity<OrderDTO>();
            order.ToTable("Orders", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            order.HasKey(x => x.Id); // Auftragsnummer
            order.Property(x => x.Date).IsRequired(true);
            order.HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

            var orderposition = modelBuilder.Entity<OrderPositionDTO>();
            orderposition.ToTable("OrderPositions", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            orderposition.HasKey(x => x.Id);
            orderposition.Property(x => x.Quantity).IsRequired(true);
            orderposition.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId); // Eine Position hat nur eine Order
            orderposition.HasOne(x => x.Article).WithMany().HasForeignKey(x => x.ArticleId); // Eine Position hat nur einen Artikel

            var invoice = modelBuilder.Entity<InvoiceDTO>();
            invoice.ToTable("Invoices", b => b.IsTemporal(
                b =>
                {
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }));
            invoice.HasKey(x => x.Id); // Rechnungsnummer
            invoice.Property(x => x.Date).IsRequired(true);
            invoice.HasOne(x => x.Customer);

            modelBuilder.Entity<ArticleDTO>().Navigation(a => a.ArticleGroup).AutoInclude();
            modelBuilder.Entity<CustomerDTO>().Navigation(c => c.Address).AutoInclude();
            modelBuilder.Entity<OrderDTO>().Navigation(o => o.Customer).AutoInclude();
        }
    }
}
