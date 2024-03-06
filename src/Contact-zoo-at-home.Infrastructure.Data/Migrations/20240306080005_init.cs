using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact_zoo_at_home.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusOfTheContract = table.Column<int>(type: "int", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ProfileImage = table.Column<byte[]>(type: "varbinary(max)", maxLength: 1048576, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NotificationOptions_OtherPhones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationOptions_NotifyOnTelegram = table.Column<bool>(type: "bit", nullable: false),
                    NotificationOptions_NotifyOnPhone = table.Column<bool>(type: "bit", nullable: false),
                    NotificationOptions_NotifyOnViber = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    RatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPetOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPetOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPetOwners_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalShelters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalShelters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalShelters_Companies_Id",
                        column: x => x.Id,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPetRepresentatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyRepresentedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPetRepresentatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPetRepresentatives_Companies_CompanyRepresentedId",
                        column: x => x.CompanyRepresentedId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyPetRepresentatives_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZooShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZooShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZooShops_Companies_Id",
                        column: x => x.Id,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubSpecies = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    CurrentPetStatus = table.Column<int>(type: "int", nullable: false),
                    RestorationTimeInDays = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    RatedBy = table.Column<int>(type: "int", nullable: false),
                    AnimalShelterId = table.Column<int>(type: "int", nullable: true),
                    BaseContractId = table.Column<int>(type: "int", nullable: true),
                    IndividualPetOwnerId = table.Column<int>(type: "int", nullable: true),
                    ZooShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_AnimalShelters_AnimalShelterId",
                        column: x => x.AnimalShelterId,
                        principalTable: "AnimalShelters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_BaseContract_BaseContractId",
                        column: x => x.BaseContractId,
                        principalTable: "BaseContract",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_IndividualPetOwners_IndividualPetOwnerId",
                        column: x => x.IndividualPetOwnerId,
                        principalTable: "IndividualPetOwners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_ZooShops_ZooShopId",
                        column: x => x.ZooShopId,
                        principalTable: "ZooShops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BaseComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CommentTargetId = table.Column<int>(type: "int", nullable: true),
                    AnswerToId = table.Column<int>(type: "int", nullable: true),
                    UserComment_CommentTargetId = table.Column<int>(type: "int", nullable: true),
                    CommentRating = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseComment_BaseComment_AnswerToId",
                        column: x => x.AnswerToId,
                        principalTable: "BaseComment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseComment_Pets_CommentTargetId",
                        column: x => x.CommentTargetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseComment_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseComment_Users_UserComment_CommentTargetId",
                        column: x => x.UserComment_CommentTargetId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExtraPetOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionLanguage = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraPetOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraPetOption_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PetBlockedDate",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetBlockedDate", x => new { x.PetId, x.Id });
                    table.ForeignKey(
                        name: "FK_PetBlockedDate_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseComment_AnswerToId",
                table: "BaseComment",
                column: "AnswerToId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseComment_AuthorId",
                table: "BaseComment",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseComment_CommentTargetId",
                table: "BaseComment",
                column: "CommentTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseComment_UserComment_CommentTargetId",
                table: "BaseComment",
                column: "UserComment_CommentTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPetRepresentatives_CompanyRepresentedId",
                table: "CompanyPetRepresentatives",
                column: "CompanyRepresentedId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraPetOption_PetId",
                table: "ExtraPetOption",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AnimalShelterId",
                table: "Pets",
                column: "AnimalShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BaseContractId",
                table: "Pets",
                column: "BaseContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_IndividualPetOwnerId",
                table: "Pets",
                column: "IndividualPetOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ZooShopId",
                table: "Pets",
                column: "ZooShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseComment");

            migrationBuilder.DropTable(
                name: "CompanyPetRepresentatives");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ExtraPetOption");

            migrationBuilder.DropTable(
                name: "PetBlockedDate");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "AnimalShelters");

            migrationBuilder.DropTable(
                name: "BaseContract");

            migrationBuilder.DropTable(
                name: "IndividualPetOwners");

            migrationBuilder.DropTable(
                name: "ZooShops");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
