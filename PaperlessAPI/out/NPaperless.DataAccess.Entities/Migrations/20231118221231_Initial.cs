using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NPaperless.DataAccess.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Correspondents",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    slug = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    match = table.Column<string>(type: "text", nullable: false),
                    matching_algorithm = table.Column<long>(type: "bigint", nullable: false),
                    is_insensitive = table.Column<bool>(type: "boolean", nullable: false),
                    document_count = table.Column<long>(type: "bigint", nullable: false),
                    last_correspondence = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correspondents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Correspondent = table.Column<long>(type: "bigint", nullable: true),
                    DocumentType = table.Column<long>(type: "bigint", nullable: true),
                    StoragePath = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Tags = table.Column<long[]>(type: "bigint[]", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArchiveSerialNumber = table.Column<string>(type: "text", nullable: true),
                    OriginalFileName = table.Column<string>(type: "text", nullable: false),
                    ArchivedFileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Correspondents");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
