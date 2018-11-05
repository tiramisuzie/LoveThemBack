﻿// <auto-generated />
using LoveThemBackAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoveThemBackAPI.Migrations
{
    [DbContext(typeof(LoveThemBackAPIDbContext))]
    [Migration("20181105223012_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoveThemBackAPI.Models.Pet", b =>
                {
                    b.Property<int>("PetID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Sex");

                    b.HasKey("PetID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("LoveThemBackAPI.Models.Review", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("PetID");

                    b.HasKey("UserID", "PetID");

                    b.HasIndex("PetID")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("LoveThemBackAPI.Models.Review", b =>
                {
                    b.HasOne("LoveThemBackAPI.Models.Pet")
                        .WithOne("Review")
                        .HasForeignKey("LoveThemBackAPI.Models.Review", "PetID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}