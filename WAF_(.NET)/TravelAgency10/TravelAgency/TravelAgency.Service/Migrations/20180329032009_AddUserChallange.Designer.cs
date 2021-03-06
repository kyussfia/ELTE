﻿// <auto-generated />
using ELTE.TravelAgency.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ELTE.TravelAgency.Service.Migrations
{
    [DbContext(typeof(TravelAgencyContext))]
    [Migration("20180329032009_AddUserChallange")]
    partial class AddUserChallange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ELTE.TravelAgency.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BuildingId");

                    b.Property<string>("Comment");

                    b.Property<int>("Price");

                    b.Property<int>("Room");

                    b.Property<int>("Turnday");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Comment");

                    b.Property<double>("LocationX");

                    b.Property<double>("LocationY");

                    b.Property<string>("Name");

                    b.Property<int>("SeaDistance");

                    b.Property<int>("ShoreId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.BuildingImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BuildingId");

                    b.Property<byte[]>("ImageLarge");

                    b.Property<byte[]>("ImageSmall");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("BuildingImages");
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserChallange");

                    b.Property<string>("UserName");

                    b.Property<byte[]>("UserPassword");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApartmentId");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.Apartment", b =>
                {
                    b.HasOne("ELTE.TravelAgency.Models.Building", "Building")
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.Building", b =>
                {
                    b.HasOne("ELTE.TravelAgency.Models.City", "City")
                        .WithMany("Buildings")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.BuildingImage", b =>
                {
                    b.HasOne("ELTE.TravelAgency.Models.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ELTE.TravelAgency.Models.Rent", b =>
                {
                    b.HasOne("ELTE.TravelAgency.Models.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ELTE.TravelAgency.Models.Guest", "Guest")
                        .WithMany("Rents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
