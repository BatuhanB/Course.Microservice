﻿// <auto-generated />
using System;
using Course.Invoice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Course.Invoice.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240827102054_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OrderInformationId")
                        .HasColumnType("text");

                    b.HasKey("Id");

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
                        .HasColumnType("timestamp without time zone");

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

            modelBuilder.Entity("Course.Invoice.Domain.Invoice.Invoice", b =>
                {
                    b.HasOne("Course.Invoice.Domain.Invoice.OrderInformation", "OrderInformation")
                        .WithMany()
                        .HasForeignKey("OrderInformationId");

                    b.OwnsOne("Course.Invoice.Domain.Invoice.Customer", "Customer", b1 =>
                        {
                            b1.Property<string>("InvoiceId")
                                .HasColumnType("text");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("InvoiceId");

                            b1.ToTable("Invoice", "invoices");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceId");

                            b1.OwnsOne("Course.Invoice.Domain.Invoice.Address", "Address", b2 =>
                                {
                                    b2.Property<string>("CustomerInvoiceId")
                                        .HasColumnType("text");

                                    b2.Property<string>("District")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Line")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Province")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("ZipCode")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("CustomerInvoiceId");

                                    b2.ToTable("Invoice", "invoices");

                                    b2.WithOwner()
                                        .HasForeignKey("CustomerInvoiceId");
                                });

                            b1.Navigation("Address")
                                .IsRequired();
                        });

                    b.Navigation("Customer")
                        .IsRequired();

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
