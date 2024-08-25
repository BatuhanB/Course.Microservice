﻿// <auto-generated />
using System;
using Course.Invoice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Course.Invoice.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customer", "invoices");
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderInformationId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderInformationId");

                    b.ToTable("Invoice", "invoices");
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.OrderInformation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("OrderInformation", "invoices");
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderInformationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductOwnerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderInformationId");

                    b.ToTable("OrderItem", "invoices");
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.Customer", b =>
                {
                    b.OwnsOne("Course.Invoice.Domain.Invoice.Address", "Address", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("text");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Line")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer", "invoices");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.Invoice", b =>
                {
                    b.HasOne("Course.Invoice.Domain.Invoice.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Course.Invoice.Domain.Invoice.OrderInformation", "OrderInformation")
                        .WithMany()
                        .HasForeignKey("OrderInformationId");

                    b.Navigation("Customer");

                    b.Navigation("OrderInformation");
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.OrderItem", b =>
                {
                    b.HasOne("Course.Invoice.Domain.Invoice.OrderInformation", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.OrderInformation", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}