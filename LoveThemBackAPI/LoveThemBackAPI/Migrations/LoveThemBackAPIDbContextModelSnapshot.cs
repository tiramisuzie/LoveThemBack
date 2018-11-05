﻿// <auto-generated />
using System;
using LoveThemBackAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoveThemBackAPI.Migrations
{
    [DbContext(typeof(LoveThemBackAPIDbContext))]
    partial class LoveThemBackAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoveThemBackAPI.Models.Pet", b =>
                {
                    b.Property<int>("PetID");

                    b.Property<int>("Age");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ReviewPetID");

                    b.Property<int?>("ReviewUserID");

                    b.Property<int>("Sex");

                    b.HasKey("PetID");

                    b.HasIndex("ReviewPetID", "ReviewUserID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("LoveThemBackAPI.Models.Review", b =>
                {
                    b.Property<int>("PetID");

                    b.Property<int>("UserID");

                    b.Property<bool>("Affectionate");

                    b.Property<bool>("Cheery");

                    b.Property<bool>("Friendly");

                    b.Property<bool>("Healthy");

                    b.Property<bool>("HighEnergy");

                    b.Property<string>("Impression");

                    b.Property<bool>("Intelligent");

                    b.Property<bool>("Playful");

                    b.HasKey("PetID", "UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("LoveThemBackAPI.Models.Pet", b =>
                {
                    b.HasOne("LoveThemBackAPI.Models.Review", "Review")
                        .WithMany("Pet")
                        .HasForeignKey("ReviewPetID", "ReviewUserID");
                });
#pragma warning restore 612, 618
        }
    }
}
