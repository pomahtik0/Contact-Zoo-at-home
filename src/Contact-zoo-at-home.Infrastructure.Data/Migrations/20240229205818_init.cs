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
                    ProfileImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _contractorId = table.Column<int>(type: "int", nullable: true),
                    _customerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Users__contractorId",
                        column: x => x._contractorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Users__customerId",
                        column: x => x._customerId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                name: "ContractPetRepresentatives",
                columns: table => new
                {
                    BaseContractId = table.Column<int>(type: "int", nullable: false),
                    BaseUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPetRepresentatives", x => new { x.BaseContractId, x.BaseUserId });
                    table.ForeignKey(
                        name: "FK_ContractPetRepresentatives_Contracts_BaseContractId",
                        column: x => x.BaseContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractPetRepresentatives_Users_BaseUserId",
                        column: x => x.BaseUserId,
                        principalTable: "Users",
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
                        name: "FK_StandartContract_Contracts_Id",
                        column: x => x.Id,
                        principalTable: "Contracts",
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
                    Species = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
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
                        name: "FK_Pets_Contracts_BaseContractId",
                        column: x => x.BaseContractId,
                        principalTable: "Contracts",
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_ZooShops_ZooShopId",
                        column: x => x.ZooShopId,
                        principalTable: "ZooShops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPetRepresentatives_CompanyRepresentedId",
                table: "CompanyPetRepresentatives",
                column: "CompanyRepresentedId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPetRepresentatives_BaseUserId",
                table: "ContractPetRepresentatives",
                column: "BaseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts__contractorId",
                table: "Contracts",
                column: "_contractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts__customerId",
                table: "Contracts",
                column: "_customerId");

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
                name: "CompanyPetRepresentatives");

            migrationBuilder.DropTable(
                name: "ContractPetRepresentatives");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "StandartContract");

            migrationBuilder.DropTable(
                name: "AnimalShelters");

            migrationBuilder.DropTable(
                name: "IndividualPetOwners");

            migrationBuilder.DropTable(
                name: "ZooShops");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
