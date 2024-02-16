using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InivitationApplication.Migrations
{
    /// <inheritdoc />
    public partial class updatedInvitations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPeople",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Invitations",
                newName: "SongRequest");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Invitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "SongRequest",
                table: "Invitations",
                newName: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeople",
                table: "Invitations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
