using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InivitationApplication.Migrations
{
    /// <inheritdoc />
    public partial class addedstacktrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StackTrace",
                table: "LogEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StackTrace",
                table: "LogEntries");
        }
    }
}
