using Microsoft.EntityFrameworkCore.Migrations;

namespace LoveThemBackAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetID);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    PetID = table.Column<int>(nullable: false),
                    Impression = table.Column<string>(nullable: true),
                    Affectionate = table.Column<bool>(nullable: false),
                    Friendly = table.Column<bool>(nullable: false),
                    HighEnergy = table.Column<bool>(nullable: false),
                    Healthy = table.Column<bool>(nullable: false),
                    Intelligent = table.Column<bool>(nullable: false),
                    Cheery = table.Column<bool>(nullable: false),
                    Playful = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.PetID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Reviews_Pets_PetID",
                        column: x => x.PetID,
                        principalTable: "Pets",
                        principalColumn: "PetID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
