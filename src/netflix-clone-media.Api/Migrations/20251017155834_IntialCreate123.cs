using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netflix_clone_media.Api.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "CoverImageUrl",
                table: "Media",
                newName: "CoverThumnailUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImageId",
                table: "Media",
                newName: "CoverThumnailId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Media",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Media",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "CoverThumnailUrl",
                table: "Media",
                newName: "CoverImageUrl");

            migrationBuilder.RenameColumn(
                name: "CoverThumnailId",
                table: "Media",
                newName: "CoverImageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Media",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
