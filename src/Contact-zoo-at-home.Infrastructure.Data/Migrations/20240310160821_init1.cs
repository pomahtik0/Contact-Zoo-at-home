using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact_zoo_at_home.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "InnerNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NotificationTargetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InnerNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InnerNotifications_Users_NotificationTargetId",
                        column: x => x.NotificationTargetId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetOwners_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CommentTargetId = table.Column<int>(type: "int", nullable: false),
                    CommentRating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserComments_Comments_Id",
                        column: x => x.Id,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserComments_Users_CommentTargetId",
                        column: x => x.CommentTargetId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    StatusOfTheContract = table.Column<int>(type: "int", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractAdress = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseContract_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseContract_PetOwners_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "PetOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_PetOwners_Id",
                        column: x => x.Id,
                        principalTable: "PetOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPetOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPetOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPetOwners_PetOwners_Id",
                        column: x => x.Id,
                        principalTable: "PetOwners",
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
                    RatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_PetOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "PetOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandartContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandartContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandartContract_BaseContract_Id",
                        column: x => x.Id,
                        principalTable: "BaseContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyRepresentedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyWorkers_Companies_CompanyRepresentedId",
                        column: x => x.CompanyRepresentedId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyWorkers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomRepresentatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ContractToRepresentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomRepresentatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomRepresentatives_BaseContract_ContractToRepresentId",
                        column: x => x.ContractToRepresentId,
                        principalTable: "BaseContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomRepresentatives_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InnerRatingNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RateTargetPetId = table.Column<int>(type: "int", nullable: true),
                    RateTargetUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InnerRatingNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InnerRatingNotifications_InnerNotifications_Id",
                        column: x => x.Id,
                        principalTable: "InnerNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InnerRatingNotifications_Pets_RateTargetPetId",
                        column: x => x.RateTargetPetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InnerRatingNotifications_Users_RateTargetUserId",
                        column: x => x.RateTargetUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PetBlockedDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetBlockedDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetBlockedDates_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CommentTargetId = table.Column<int>(type: "int", nullable: false),
                    AnswerToId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetComments_Comments_Id",
                        column: x => x.Id,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetComments_PetComments_AnswerToId",
                        column: x => x.AnswerToId,
                        principalTable: "PetComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PetComments_Pets_CommentTargetId",
                        column: x => x.CommentTargetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PetImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 1048576, nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetImages_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionLanguage = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetOptions_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetsInContractJoin",
                columns: table => new
                {
                    BaseContractId = table.Column<int>(type: "int", nullable: false),
                    PetsInContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetsInContractJoin", x => new { x.BaseContractId, x.PetsInContractId });
                    table.ForeignKey(
                        name: "FK_PetsInContractJoin_BaseContract_BaseContractId",
                        column: x => x.BaseContractId,
                        principalTable: "BaseContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetsInContractJoin_Pets_PetsInContractId",
                        column: x => x.PetsInContractId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyWorkersInContract",
                columns: table => new
                {
                    ContractsToRepresentId = table.Column<int>(type: "int", nullable: false),
                    PetRepresentativesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkersInContract", x => new { x.ContractsToRepresentId, x.PetRepresentativesId });
                    table.ForeignKey(
                        name: "FK_CompanyWorkersInContract_BaseContract_ContractsToRepresentId",
                        column: x => x.ContractsToRepresentId,
                        principalTable: "BaseContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyWorkersInContract_CompanyWorkers_PetRepresentativesId",
                        column: x => x.PetRepresentativesId,
                        principalTable: "CompanyWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseContract_ContractorId",
                table: "BaseContract",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseContract_CustomerId",
                table: "BaseContract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkers_CompanyRepresentedId",
                table: "CompanyWorkers",
                column: "CompanyRepresentedId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkersInContract_PetRepresentativesId",
                table: "CompanyWorkersInContract",
                column: "PetRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomRepresentatives_CompanyId",
                table: "CustomRepresentatives",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomRepresentatives_ContractToRepresentId",
                table: "CustomRepresentatives",
                column: "ContractToRepresentId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerNotifications_NotificationTargetId",
                table: "InnerNotifications",
                column: "NotificationTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerRatingNotifications_RateTargetPetId",
                table: "InnerRatingNotifications",
                column: "RateTargetPetId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerRatingNotifications_RateTargetUserId",
                table: "InnerRatingNotifications",
                column: "RateTargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PetBlockedDates_PetId",
                table: "PetBlockedDates",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetComments_AnswerToId",
                table: "PetComments",
                column: "AnswerToId");

            migrationBuilder.CreateIndex(
                name: "IX_PetComments_CommentTargetId",
                table: "PetComments",
                column: "CommentTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetImages_PetId",
                table: "PetImages",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetOptions_PetId",
                table: "PetOptions",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PetsInContractJoin_PetsInContractId",
                table: "PetsInContractJoin",
                column: "PetsInContractId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_CommentTargetId",
                table: "UserComments",
                column: "CommentTargetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyWorkersInContract");

            migrationBuilder.DropTable(
                name: "CustomRepresentatives");

            migrationBuilder.DropTable(
                name: "IndividualPetOwners");

            migrationBuilder.DropTable(
                name: "InnerRatingNotifications");

            migrationBuilder.DropTable(
                name: "PetBlockedDates");

            migrationBuilder.DropTable(
                name: "PetComments");

            migrationBuilder.DropTable(
                name: "PetImages");

            migrationBuilder.DropTable(
                name: "PetOptions");

            migrationBuilder.DropTable(
                name: "PetsInContractJoin");

            migrationBuilder.DropTable(
                name: "StandartContract");

            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.DropTable(
                name: "CompanyWorkers");

            migrationBuilder.DropTable(
                name: "InnerNotifications");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "BaseContract");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PetOwners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
