using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SzczecinHackathon.Migrations
{
    /// <inheritdoc />
    public partial class alotofbugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ChatUser",
                type: "TEXT",
                nullable: true);
        }
    }
}
