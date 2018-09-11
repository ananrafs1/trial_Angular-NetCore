﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using trialApps.API.Data;

namespace trialApps.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180911122802_addPublicId")]
    partial class addPublicId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("trialApps.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastModifiedBy");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.Property<bool>("isDelete");

                    b.Property<bool>("isMain");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("trialApps.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DoB");

                    b.Property<string>("Gender");

                    b.Property<string>("Interests");

                    b.Property<string>("Intro");

                    b.Property<string>("KnownAs");

                    b.Property<DateTime>("LastActive");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastModifiedBy");

                    b.Property<string>("LookingFor");

                    b.Property<byte[]>("PassHash");

                    b.Property<byte[]>("PassSalt");

                    b.Property<string>("Username");

                    b.Property<bool>("isDelete");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("trialApps.API.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("trialApps.API.Models.Photo", b =>
                {
                    b.HasOne("trialApps.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
