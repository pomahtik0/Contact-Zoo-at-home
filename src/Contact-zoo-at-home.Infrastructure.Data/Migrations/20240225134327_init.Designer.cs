﻿// <auto-generated />
using System;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Contact_zoo_at_home.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240225134327_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.AbstractPet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnimalShelterId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IndividualPetOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RaitingUserVotesCount")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalShelterId");

                    b.HasIndex("IndividualPetOwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pets", (string)null);

                    b.HasDiscriminator<string>("Species").HasValue("AbstractPet");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.AbstractUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.Cat", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Pets.AbstractPet");

                    b.HasDiscriminator().HasValue("Cat");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.Dog", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Pets.AbstractPet");

                    b.HasDiscriminator().HasValue("Dog");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.AbstractUser");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CompanyPetRepresentative", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.AbstractUser");

                    b.Property<int>("CompanyRepresentedId")
                        .HasColumnType("int");

                    b.HasIndex("CompanyRepresentedId");

                    b.ToTable("CompanyPetRepresentative");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.AbstractUser");

                    b.ToTable("CustomerUser");

                    b.HasDiscriminator().IsComplete(true);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualPetOwner", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.AbstractUser");

                    b.ToTable("IndividualPetOwner");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.AnimalShelter", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany");

                    b.ToTable("AnimalShelter");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.ZooShop", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany");

                    b.ToTable("ZooShop");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.AbstractPet", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.AnimalShelter", null)
                        .WithMany("OwnedPets")
                        .HasForeignKey("AnimalShelterId");

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualPetOwner", null)
                        .WithMany("OwnedPets")
                        .HasForeignKey("IndividualPetOwnerId");

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.ZooShop", "Owner")
                        .WithMany("OwnedPets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractUser", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CompanyPetRepresentative", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", "CompanyRepresented")
                        .WithMany("CompanyPetRepresentatives")
                        .HasForeignKey("CompanyRepresentedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractUser", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CompanyPetRepresentative", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyRepresented");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractUser", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualPetOwner", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractUser", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualPetOwner", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.AnimalShelter", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.AnimalShelter", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.ZooShop", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.ZooShop", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.AbstractCompany", b =>
                {
                    b.Navigation("CompanyPetRepresentatives");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualPetOwner", b =>
                {
                    b.Navigation("OwnedPets");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.AnimalShelter", b =>
                {
                    b.Navigation("OwnedPets");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany.ZooShop", b =>
                {
                    b.Navigation("OwnedPets");
                });
#pragma warning restore 612, 618
        }
    }
}
