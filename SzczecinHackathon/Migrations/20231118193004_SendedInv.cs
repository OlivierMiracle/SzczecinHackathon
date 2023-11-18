using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SzczecinHackathon.Migrations
{
    /// <inheritdoc />
    public partial class SendedInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SendedInvitations",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendedInvitations",
                table: "Users");
        }
    }
}
