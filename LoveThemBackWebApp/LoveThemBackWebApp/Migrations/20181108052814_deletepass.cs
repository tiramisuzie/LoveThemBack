using Microsoft.EntityFrameworkCore.Migrations;

namespace LoveThemBackWebApp.Migrations
{
    public partial class deletepass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Profiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Profiles",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "password1");

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "password2");
        }
    }
}
