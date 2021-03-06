using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPILab.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Category", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Horror", "Halloween", 1978 },
                    { 2, "Drama", "Mr. Brooks", 2007 },
                    { 3, "SciFi", "Star Wars", 1977 },
                    { 4, "Horror", "Alien", 1979 },
                    { 5, "Comedy", "Mrs. Doubtfire", 1992 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
