using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reddit_clone.Migrations
{
    /// <inheritdoc />
    public partial class AddEngagementScoreToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVote",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UpVote",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "EngagementScore",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngagementScore",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "DownVote",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVote",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
