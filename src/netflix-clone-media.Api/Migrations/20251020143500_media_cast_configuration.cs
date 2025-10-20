using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netflix_clone_media.Api.Migrations
{
    /// <inheritdoc />
    public partial class media_cast_configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaCasts_Media_MediaId",
                table: "MediaCasts");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaCasts_Person_PersonId",
                table: "MediaCasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaCasts",
                table: "MediaCasts");

            migrationBuilder.DropIndex(
                name: "IX_MediaCasts_MediaId",
                table: "MediaCasts");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "MediaCasts",
                newName: "MediaCast");

            migrationBuilder.RenameIndex(
                name: "IX_MediaCasts_PersonId",
                table: "MediaCast",
                newName: "IX_MediaCast_PersonId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Media",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaCast",
                table: "MediaCast",
                columns: new[] { "MediaId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MediaCast_Media_MediaId",
                table: "MediaCast",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaCast_Person_PersonId",
                table: "MediaCast",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaCast_Media_MediaId",
                table: "MediaCast");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaCast_Person_PersonId",
                table: "MediaCast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaCast",
                table: "MediaCast");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "MediaCast",
                newName: "MediaCasts");

            migrationBuilder.RenameIndex(
                name: "IX_MediaCast_PersonId",
                table: "MediaCasts",
                newName: "IX_MediaCasts_PersonId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Media",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaCasts",
                table: "MediaCasts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MediaCasts_MediaId",
                table: "MediaCasts",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaCasts_Media_MediaId",
                table: "MediaCasts",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaCasts_Person_PersonId",
                table: "MediaCasts",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
