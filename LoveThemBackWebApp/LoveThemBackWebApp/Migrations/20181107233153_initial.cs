using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoveThemBackWebApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LocationZip = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    PetID = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.UserID, x.PetID });
                    table.ForeignKey(
                        name: "FK_Favorites_Profiles_UserID",
                        column: x => x.UserID,
                        principalTable: "Profiles",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "UserID", "LocationZip", "Password", "Username" },
                values: new object[] { 1, 98144, "password1", "username1" });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "UserID", "LocationZip", "Password", "Username" },
                values: new object[] { 2, 98144, "password2", "username2" });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "UserID", "PetID", "Notes" },
                values: new object[] { 1, 1, "notes 1" });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "UserID", "PetID", "Notes" },
                values: new object[] { 1, 2, "notes 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
