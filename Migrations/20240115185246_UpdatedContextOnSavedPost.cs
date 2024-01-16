using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reddit_clone.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContextOnSavedPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPost_ApplicationUsers_ApplicationUserId",
                table: "SavedPost");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPost_Posts_PostId",
                table: "SavedPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedPost",
                table: "SavedPost");

            migrationBuilder.RenameTable(
                name: "SavedPost",
                newName: "SavedPosts");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPost_PostId",
                table: "SavedPosts",
                newName: "IX_SavedPosts_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts",
                columns: new[] { "ApplicationUserId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPosts_ApplicationUsers_ApplicationUserId",
                table: "SavedPosts",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPosts_Posts_PostId",
                table: "SavedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPosts_ApplicationUsers_ApplicationUserId",
                table: "SavedPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPosts_Posts_PostId",
                table: "SavedPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts");

            migrationBuilder.RenameTable(
                name: "SavedPosts",
                newName: "SavedPost");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPosts_PostId",
                table: "SavedPost",
                newName: "IX_SavedPost_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedPost",
                table: "SavedPost",
                columns: new[] { "ApplicationUserId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPost_ApplicationUsers_ApplicationUserId",
                table: "SavedPost",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPost_Posts_PostId",
                table: "SavedPost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
