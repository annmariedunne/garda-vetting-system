using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardaVettingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    ApplicantNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MothersLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.ApplicantNumber);
                });

            migrationBuilder.CreateTable(
                name: "AccessCodes",
                columns: table => new
                {
                    AccessCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantNumber = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessCodes", x => x.AccessCodeId);
                    table.ForeignKey(
                        name: "FK_AccessCodes_Applicants_ApplicantNumber",
                        column: x => x.ApplicantNumber,
                        principalTable: "Applicants",
                        principalColumn: "ApplicantNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantAddresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantNumber = table.Column<int>(type: "int", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResidentFrom = table.Column<int>(type: "int", nullable: false),
                    ResidentTo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantAddresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_ApplicantAddresses_Applicants_ApplicantNumber",
                        column: x => x.ApplicantNumber,
                        principalTable: "Applicants",
                        principalColumn: "ApplicantNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessCodes_ApplicantNumber",
                table: "AccessCodes",
                column: "ApplicantNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantAddresses_ApplicantNumber",
                table: "ApplicantAddresses",
                column: "ApplicantNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessCodes");

            migrationBuilder.DropTable(
                name: "ApplicantAddresses");

            migrationBuilder.DropTable(
                name: "Applicants");
        }
    }
}
