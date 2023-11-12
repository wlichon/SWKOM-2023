using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPaperless.Services.Migrations
{
    /// <inheritdoc />
    public partial class newCorrespondent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Correspondents",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Correspondents",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Match",
                table: "Correspondents",
                newName: "match");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Correspondents",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MatchingAlgorithm",
                table: "Correspondents",
                newName: "matching_algorithm");

            migrationBuilder.RenameColumn(
                name: "LastCorrespondence",
                table: "Correspondents",
                newName: "last_correspondence");

            migrationBuilder.RenameColumn(
                name: "IsInsensitive",
                table: "Correspondents",
                newName: "is_insensitive");

            migrationBuilder.RenameColumn(
                name: "DocumentCount",
                table: "Correspondents",
                newName: "document_count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "slug",
                table: "Correspondents",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Correspondents",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "match",
                table: "Correspondents",
                newName: "Match");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Correspondents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "matching_algorithm",
                table: "Correspondents",
                newName: "MatchingAlgorithm");

            migrationBuilder.RenameColumn(
                name: "last_correspondence",
                table: "Correspondents",
                newName: "LastCorrespondence");

            migrationBuilder.RenameColumn(
                name: "is_insensitive",
                table: "Correspondents",
                newName: "IsInsensitive");

            migrationBuilder.RenameColumn(
                name: "document_count",
                table: "Correspondents",
                newName: "DocumentCount");
        }
    }
}
