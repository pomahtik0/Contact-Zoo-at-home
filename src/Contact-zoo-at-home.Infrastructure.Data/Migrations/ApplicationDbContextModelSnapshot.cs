﻿// <auto-generated />
using System;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Contact_zoo_at_home.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Comments.BaseComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Comments", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Contracts.BaseContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityType")
                        .HasColumnType("int");

                    b.Property<string>("ContractAdress")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime?>("ContractDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ContractorId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("RepresentativeId")
                        .HasColumnType("int");

                    b.Property<int>("StatusOfTheContract")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RepresentativeId");

                    b.HasIndex("StatusOfTheContract");

                    b.ToTable("Contracts", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Notifications.InnerNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NotificationTargetId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTargetId");

                    b.ToTable("InnerNotifications");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Notifications.NotificationOptions", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("Email")
                        .HasColumnType("bit");

                    b.Property<bool>("Telegram")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("NotificationOptions");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.ExtraPetOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("ExtraPetOption");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityType")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPetStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentRating")
                        .HasColumnType("decimal(8, 5)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal");

                    b.Property<int>("RatedBy")
                        .HasColumnType("int");

                    b.Property<int>("RestorationTimeInDays")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Pets", (string)null);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.PetBlockedDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BlockedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("Reason")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("PetBlockedDate");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.PetImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("PetImages");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.PetSpecies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Species", (string)null);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.BaseUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentRating")
                        .HasColumnType("decimal(8, 5)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Images.CompanyImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyImage");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Images.ProfileImage", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("ProfileImage");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Representative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("RepresentativesOfCompanies", (string)null);
                });

            modelBuilder.Entity("PetsInContract", b =>
                {
                    b.Property<int>("BaseContractId")
                        .HasColumnType("int");

                    b.Property<int>("PetsInContractId")
                        .HasColumnType("int");

                    b.HasKey("BaseContractId", "PetsInContractId");

                    b.HasIndex("PetsInContractId");

                    b.ToTable("PetsInContract");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Comments.PetComment", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Comments.BaseComment");

                    b.Property<int?>("AnswerToId")
                        .HasColumnType("int");

                    b.Property<int>("CommentTargetId")
                        .HasColumnType("int");

                    b.HasIndex("AnswerToId");

                    b.HasIndex("CommentTargetId");

                    b.ToTable("PetComments");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Comments.UserComment", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Comments.BaseComment");

                    b.Property<decimal>("CommentRating")
                        .HasColumnType("decimal(8, 5)");

                    b.Property<int>("CommentTargetId")
                        .HasColumnType("int");

                    b.HasIndex("CommentTargetId");

                    b.ToTable("UserComments");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Contracts.StandartContract", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Contracts.BaseContract");

                    b.ToTable("StandartContracts");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Notifications.InnerRatingNotification", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Notifications.InnerNotification");

                    b.Property<int?>("RateTargetPetId")
                        .HasColumnType("int");

                    b.Property<int?>("RateTargetUserId")
                        .HasColumnType("int");

                    b.HasIndex("RateTargetPetId");

                    b.HasIndex("RateTargetUserId");

                    b.ToTable("InnerRatingNotifications");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.BaseUser");

                    b.ToTable("PetOwners", (string)null);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.BaseUser");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.Company", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualOwner", b =>
                {
                    b.HasBaseType("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.ToTable("IndividualOwners", (string)null);
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Comments.BaseComment", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", "Author")
                        .WithMany("MyComments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Contracts.BaseContract", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", "Contractor")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.Representative", "Representative")
                        .WithMany("ContractsToRepresent")
                        .HasForeignKey("RepresentativeId");

                    b.Navigation("Contractor");

                    b.Navigation("Customer");

                    b.Navigation("Representative");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Notifications.InnerNotification", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", "NotificationTarget")
                        .WithMany()
                        .HasForeignKey("NotificationTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationTarget");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Notifications.NotificationOptions", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", null)
                        .WithOne("NotificationOptions")
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Notifications.NotificationOptions", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.ExtraPetOption", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.Pet", null)
                        .WithMany("PetOptions")
                        .HasForeignKey("PetId");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.Pet", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", "Owner")
                        .WithMany("OwnedPets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.PetSpecies", "Species")
                        .WithMany()
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.PetBlockedDate", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.Pet", null)
                        .WithMany("BlockedDates")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.PetImage", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.Pet", null)
                        .WithMany("Images")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Images.CompanyImage", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.Company", null)
                        .WithMany("Images")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Images.ProfileImage", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", null)
                        .WithOne("ProfileImage")
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.Images.ProfileImage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Representative", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.Company", "Company")
                        .WithMany("Representatives")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("PetsInContract", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Contracts.BaseContract", null)
                        .WithMany()
                        .HasForeignKey("BaseContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.Pet", null)
                        .WithMany()
                        .HasForeignKey("PetsInContractId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Comments.PetComment", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Comments.PetComment", "AnswerTo")
                        .WithMany()
                        .HasForeignKey("AnswerToId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.Pet", "CommentTarget")
                        .WithMany("Comments")
                        .HasForeignKey("CommentTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Comments.BaseComment", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Comments.PetComment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerTo");

                    b.Navigation("CommentTarget");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Comments.UserComment", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", "CommentTarget")
                        .WithMany("Comments")
                        .HasForeignKey("CommentTargetId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Comments.BaseComment", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Comments.UserComment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentTarget");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Contracts.StandartContract", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Contracts.BaseContract", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Contracts.StandartContract", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Notifications.InnerRatingNotification", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Notifications.InnerNotification", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Notifications.InnerRatingNotification", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Pets.Pet", "RateTargetPet")
                        .WithMany()
                        .HasForeignKey("RateTargetPetId");

                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", "RateTargetUser")
                        .WithMany()
                        .HasForeignKey("RateTargetUserId");

                    b.Navigation("RateTargetPet");

                    b.Navigation("RateTargetUser");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.Company", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.Company", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualOwner", b =>
                {
                    b.HasOne("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", null)
                        .WithOne()
                        .HasForeignKey("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.IndividualOwner", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Pets.Pet", b =>
                {
                    b.Navigation("BlockedDates");

                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("PetOptions");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.BaseUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("MyComments");

                    b.Navigation("NotificationOptions")
                        .IsRequired();

                    b.Navigation("ProfileImage")
                        .IsRequired();
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.Representative", b =>
                {
                    b.Navigation("ContractsToRepresent");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.BasePetOwner", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("OwnedPets");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CustomerUser", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.Company", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Representatives");
                });
#pragma warning restore 612, 618
        }
    }
}
