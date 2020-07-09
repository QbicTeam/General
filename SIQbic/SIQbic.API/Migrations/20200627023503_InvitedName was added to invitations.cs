using Microsoft.EntityFrameworkCore.Migrations;

namespace SIQbic.API.Migrations
{
    public partial class InvitedNamewasaddedtoinvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvetedName",
                table: "Invitations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvetedName",
                table: "Invitations");
        }
    }
}
