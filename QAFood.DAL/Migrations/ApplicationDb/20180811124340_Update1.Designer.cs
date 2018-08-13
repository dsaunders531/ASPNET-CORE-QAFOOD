﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QAFood.DAL;

namespace QAFood.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180811124340_Update1")]
    partial class Update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QAFood.DAL.Models.FoodItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<long>("FoodParcelId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("FoodParcelId");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("QAFood.DAL.Models.FoodParcel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime>("PostedDate");

                    b.HasKey("Id");

                    b.ToTable("FoodParcels");
                });

            modelBuilder.Entity("QAFood.DAL.Models.TestItemCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("TestItemCategories");
                });

            modelBuilder.Entity("QAFood.DAL.Models.TestResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("FoodParcelId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime>("TestDate");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FoodParcelId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("QAFood.DAL.Models.TestResultItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryId");

                    b.Property<long>("FoodItemId");

                    b.Property<byte>("Result");

                    b.Property<long>("TestResultId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("TestResultId");

                    b.ToTable("TestResultItems");
                });

            modelBuilder.Entity("QAFood.DAL.Models.FoodItem", b =>
                {
                    b.HasOne("QAFood.DAL.Models.FoodParcel", "FoodParcel")
                        .WithMany("FoodItems")
                        .HasForeignKey("FoodParcelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QAFood.DAL.Models.TestResult", b =>
                {
                    b.HasOne("QAFood.DAL.Models.FoodParcel", "FoodParcel")
                        .WithMany("TestResults")
                        .HasForeignKey("FoodParcelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QAFood.DAL.Models.TestResultItem", b =>
                {
                    b.HasOne("QAFood.DAL.Models.TestItemCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QAFood.DAL.Models.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QAFood.DAL.Models.TestResult", "TestResult")
                        .WithMany("TestResultItems")
                        .HasForeignKey("TestResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
