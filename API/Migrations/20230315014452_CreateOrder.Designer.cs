﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(FlightsContext))]
    [Migration("20230315014452_CreateOrder")]
    partial class CreateOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Aggregates.AirportAggregate.Airport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Domain.Aggregates.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Arrival")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("Departure")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DestinationAirportId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OriginAirportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("OriginAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.FlightRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Available")
                        .HasColumnType("integer");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("FlightRates");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.OrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FlightRateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("FlightRateId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.Flight", b =>
                {
                    b.HasOne("Domain.Aggregates.AirportAggregate.Airport", null)
                        .WithMany()
                        .HasForeignKey("DestinationAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Aggregates.AirportAggregate.Airport", null)
                        .WithMany()
                        .HasForeignKey("OriginAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.FlightRate", b =>
                {
                    b.HasOne("Domain.Aggregates.FlightAggregate.Flight", null)
                        .WithMany("Rates")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Common.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("FlightRateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric");

                            b1.HasKey("FlightRateId");

                            b1.ToTable("FlightRates");

                            b1.WithOwner()
                                .HasForeignKey("FlightRateId");
                        });

                    b.Navigation("Price");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.HasOne("Domain.Aggregates.CustomerAggregate.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.OrderLine", b =>
                {
                    b.HasOne("Domain.Aggregates.FlightAggregate.FlightRate", "FlightRate")
                        .WithMany()
                        .HasForeignKey("FlightRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Aggregates.OrderAggregate.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlightRate");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.Flight", b =>
                {
                    b.Navigation("Rates");
                });

            modelBuilder.Entity("Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
