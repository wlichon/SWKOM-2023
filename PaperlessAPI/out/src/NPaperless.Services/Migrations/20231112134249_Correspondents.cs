using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NPaperless.Services.Migrations
{
    /// <inheritdoc />
    public partial class Correspondents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoragePaths");

            migrationBuilder.CreateTable(
                name: "Correspondents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Slug = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Match = table.Column<string>(type: "text", nullable: true),
                    MatchingAlgorithm = table.Column<long>(type: "bigint", nullable: false),
                    IsInsensitive = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentCount = table.Column<long>(type: "bigint", nullable: false),
                    LastCorrespondence = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correspondents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Correspondents");

            migrationBuilder.CreateTable(
                name: "StoragePaths",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IsInsensitive = table.Column<bool>(type: "boolean", nullable: false),
                    Match = table.Column<string>(type: "text", nullable: false),
                    MatchingAlgorithm = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Owner = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePaths", x => x.Id);
                });
        }
    }
}
