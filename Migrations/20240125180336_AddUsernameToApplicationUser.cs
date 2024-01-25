using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reddit_clone.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ApplicationUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "ApplicationUsers");
        }
    }
}
