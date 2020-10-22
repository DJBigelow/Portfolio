﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Portfolio.Shared.Data;

namespace Portfolio.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201005171916_CategoriesRework")]
    partial class CategoriesRework
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Portfolio.Shared.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Portfolio.Shared.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Design")
                        .HasColumnType("text");

                    b.Property<string>("Requirement")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Portfolio.Shared.Models.ProjectCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectID")
                        .HasColumnType("integer");

                    b.Property<int>("ID")
                        .HasColumnType("integer");

                    b.HasKey("CategoryID", "ProjectID");

                    b.HasIndex("ProjectID");

                    b.ToTable("ProjectCategories");
                });

            modelBuilder.Entity("Portfolio.Shared.Models.ProjectCategory", b =>
                {
                    b.HasOne("Portfolio.Shared.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portfolio.Shared.Models.Project", "Project")
                        .WithMany("ProjectCategories")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}