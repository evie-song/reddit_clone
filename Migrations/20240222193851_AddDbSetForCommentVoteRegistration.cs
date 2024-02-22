using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reddit_clone.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSetForCommentVoteRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVoteRegistration_ApplicationUsers_ApplicationUserId",
                table: "CommentVoteRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentVoteRegistration_Comments_CommentId",
                table: "CommentVoteRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentVoteRegistration",
                table: "CommentVoteRegistration");

            migrationBuilder.RenameTable(
                name: "CommentVoteRegistration",
                newName: "CommentVoteRegistrations");

            migrationBuilder.RenameIndex(
                name: "IX_CommentVoteRegistration_CommentId",
                table: "CommentVoteRegistrations",
                newName: "IX_CommentVoteRegistrations_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentVoteRegistrations",
                table: "CommentVoteRegistrations",
                columns: new[] { "ApplicationUserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVoteRegistrations_ApplicationUsers_ApplicationUserId",
                table: "CommentVoteRegistrations",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVoteRegistrations_Comments_CommentId",
                table: "CommentVoteRegistrations",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVoteRegistrations_ApplicationUsers_ApplicationUserId",
                table: "CommentVoteRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentVoteRegistrations_Comments_CommentId",
                table: "CommentVoteRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentVoteRegistrations",
                table: "CommentVoteRegistrations");

            migrationBuilder.RenameTable(
                name: "CommentVoteRegistrations",
                newName: "CommentVoteRegistration");

            migrationBuilder.RenameIndex(
                name: "IX_CommentVoteRegistrations_CommentId",
                table: "CommentVoteRegistration",
                newName: "IX_CommentVoteRegistration_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentVoteRegistration",
                table: "CommentVoteRegistration",
                columns: new[] { "ApplicationUserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVoteRegistration_ApplicationUsers_ApplicationUserId",
                table: "CommentVoteRegistration",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVoteRegistration_Comments_CommentId",
                table: "CommentVoteRegistration",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
