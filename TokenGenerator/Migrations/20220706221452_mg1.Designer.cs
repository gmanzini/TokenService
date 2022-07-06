﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokenGenerator.Data;

namespace TokenGeneratorService.Migrations
{
    [DbContext(typeof(TokenGeneratorContext))]
    [Migration("20220706221452_mg1")]
    partial class mg1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TokenGenerator.CustomerDTO", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.HasKey("CustomerID");

                    b.HasIndex("CardId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TokenGeneratorService.Domain.CardDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Token")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("TokenGenerator.CustomerDTO", b =>
                {
                    b.HasOne("TokenGeneratorService.Domain.CardDTO", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");
                });
#pragma warning restore 612, 618
        }
    }
}
