using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SzczecinHackathon.Migrations
{
    /// <inheritdoc />
    public partial class AddsHappenings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Happenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ImgPath = table.Column<string>(type: "TEXT", nullable: false),
                    IsHidden = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Happenings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HappeningUser",
                columns: table => new
                {
                    HappeningId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HappeningUser", x => new { x.HappeningId, x.UserId });
                    table.ForeignKey(
                        name: "FK_HappeningUser_Happenings_HappeningId",
                        column: x => x.HappeningId,
                        principalTable: "Happenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HappeningUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HappeningUser_UserId",
                table: "HappeningUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HappeningUser");

            migrationBuilder.DropTable(
                name: "Happenings");
        }
    }
}
