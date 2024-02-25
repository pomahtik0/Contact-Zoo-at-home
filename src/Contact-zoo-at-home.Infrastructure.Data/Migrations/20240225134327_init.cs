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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "CustomerUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerUser_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPetOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPetOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPetOwner_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalShelter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalShelter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalShelter_Companies_Id",
                        column: x => x.Id,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPetRepresentative",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyRepresentedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPetRepresentative", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPetRepresentative_Companies_CompanyRepresentedId",
                        column: x => x.CompanyRepresentedId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyPetRepresentative_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZooShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZooShop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZooShop_Companies_Id",
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
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    RaitingUserVotesCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AnimalShelterId = table.Column<int>(type: "int", nullable: true),
                    IndividualPetOwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_AnimalShelter_AnimalShelterId",
                        column: x => x.AnimalShelterId,
                        principalTable: "AnimalShelter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_IndividualPetOwner_IndividualPetOwnerId",
                        column: x => x.IndividualPetOwnerId,
                        principalTable: "IndividualPetOwner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_ZooShop_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ZooShop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPetRepresentative_CompanyRepresentedId",
                table: "CompanyPetRepresentative",
                column: "CompanyRepresentedId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AnimalShelterId",
                table: "Pets",
                column: "AnimalShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_IndividualPetOwnerId",
                table: "Pets",
                column: "IndividualPetOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPetRepresentative");

            migrationBuilder.DropTable(
                name: "CustomerUser");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "AnimalShelter");

            migrationBuilder.DropTable(
                name: "IndividualPetOwner");

            migrationBuilder.DropTable(
                name: "ZooShop");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
