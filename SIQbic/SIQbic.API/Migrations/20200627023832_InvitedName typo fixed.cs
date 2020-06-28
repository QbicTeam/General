using Microsoft.EntityFrameworkCore.Migrations;

namespace SIQbic.API.Migrations
{
    public partial class InvitedNametypofixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvetedName",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "InvitedName",
                table: "Invitations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedName",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "InvetedName",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
