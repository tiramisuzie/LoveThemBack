using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoveThemBackAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ReviewPetID = table.Column<int>(nullable: true),
                    ReviewUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetID);
                    table.ForeignKey(
                        name: "FK_Pets_Reviews_ReviewPetID_ReviewUserID",
                        columns: x => new { x.ReviewPetID, x.ReviewUserID },
                        principalTable: "Reviews",
                        principalColumns: new[] { "PetID", "UserID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ReviewPetID_ReviewUserID",
                table: "Pets",
                columns: new[] { "ReviewPetID", "ReviewUserID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
