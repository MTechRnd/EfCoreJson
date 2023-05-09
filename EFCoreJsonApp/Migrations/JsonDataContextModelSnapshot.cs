﻿// <auto-generated />
using System;
using EFCoreJsonApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreJsonApp.Migrations
{
    [DbContext(typeof(JsonDataContext))]
    partial class JsonDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreJsonApp.Models.OrderWithOrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("OrderWithOrderDetails");
                });

            modelBuilder.Entity("EFCoreJsonApp.Models.OrderWithOrderDetails", b =>
                {
                    b.OwnsMany("EFCoreJsonApp.Models.OrderDetailsJson", "OrderDetailsJson", b1 =>
                        {
                            b1.Property<int>("OrderWithOrderDetailsId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("ItemName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<float>("Price")
                                .HasColumnType("real");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.Property<float>("Total")
                                .HasColumnType("real");

                            b1.HasKey("OrderWithOrderDetailsId", "Id");

                            b1.ToTable("OrderWithOrderDetails");

                            b1.ToJson("OrderDetailsJson");

                            b1.WithOwner()
                                .HasForeignKey("OrderWithOrderDetailsId");
                        });

                    b.Navigation("OrderDetailsJson");
                });
#pragma warning restore 612, 618
        }
    }
}