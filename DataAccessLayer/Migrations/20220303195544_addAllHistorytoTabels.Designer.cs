﻿// <auto-generated />
using System;
using DataAccessLayer;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(SetupDB))]
    [Migration("20220303195544_addAllHistorytoTabels")]
    partial class addAllHistorytoTabels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccessLayer.Models.AddressDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Countryname")
                        .HasColumnType("int");

                    b.Property<string>("Plz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.HasKey("Id");

                    b.ToTable("Addresses", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.ArticleDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArticleGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("ArticleGroupId");

                    b.ToTable("Articles", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.ArticleGroupDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentArticleGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.HasKey("Id");

                    b.ToTable("ArticelGroups", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.CustomerDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Customer", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.InvoiceDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Brutto")
                        .HasColumnType("float");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<double>("Netto")
                        .HasColumnType("float");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoices", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.OrderDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.OrderPositionDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime>("ValidTo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderPositions", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("ValidFrom")
                                .HasColumnName("ValidFrom");
                            ttb
                                .HasPeriodEnd("ValidTo")
                                .HasColumnName("ValidTo");
                        }
                    ));
                });

            modelBuilder.Entity("DataAccessLayer.Models.ArticleDTO", b =>
                {
                    b.HasOne("DataAccessLayer.Models.ArticleGroupDTO", "ArticleGroup")
                        .WithMany()
                        .HasForeignKey("ArticleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleGroup");
                });

            modelBuilder.Entity("DataAccessLayer.Models.CustomerDTO", b =>
                {
                    b.HasOne("DataAccessLayer.Models.AddressDTO", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DataAccessLayer.Models.InvoiceDTO", b =>
                {
                    b.HasOne("DataAccessLayer.Models.CustomerDTO", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DataAccessLayer.Models.OrderDTO", b =>
                {
                    b.HasOne("DataAccessLayer.Models.CustomerDTO", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DataAccessLayer.Models.OrderPositionDTO", b =>
                {
                    b.HasOne("DataAccessLayer.Models.ArticleDTO", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.OrderDTO", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
