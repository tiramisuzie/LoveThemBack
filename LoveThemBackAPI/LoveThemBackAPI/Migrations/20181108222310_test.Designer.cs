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
    [Migration("20181108222310_test")]
    partial class test
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
                    b.Property<int>("PetID");

                    b.Property<string>("Address");

                    b.Property<string>("Age");

                    b.Property<string>("Animal");

                    b.Property<string>("Breed");

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("Mix");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Photos");

                    b.Property<string>("Sex");

                    b.Property<string>("ShelterID");

                    b.Property<string>("ShelterName");

                    b.Property<string>("Size");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("PetID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("LoveThemBackAPI.Models.Review", b =>
                {
                    b.Property<int>("PetID");

                    b.Property<int>("UserID");

                    b.Property<bool>("Affectionate");

                    b.Property<bool>("Cheery");

                    b.Property<bool>("Drool");

                    b.Property<bool>("Friendly");

                    b.Property<bool>("Healthy");

                    b.Property<bool>("HighEnergy");

                    b.Property<string>("Impression");

                    b.Property<bool>("Intelligent");

                    b.Property<bool>("Playful");

                    b.HasKey("PetID", "UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("LoveThemBackAPI.Models.Review", b =>
                {
                    b.HasOne("LoveThemBackAPI.Models.Pet")
                        .WithMany("Reviews")
                        .HasForeignKey("PetID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
