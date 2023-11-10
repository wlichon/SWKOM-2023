using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPaperless.Services.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoragePaths",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    MatchingAlgorithm = table.Column<int>(type: "integer", nullable: false),
                    Match = table.Column<string>(type: "text", nullable: false),
                    IsInsensitive = table.Column<bool>(type: "boolean", nullable: false),
                    Owner = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePaths", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoragePaths");
        }
    }
}
