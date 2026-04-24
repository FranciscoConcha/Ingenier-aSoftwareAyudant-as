using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDivine.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class FuntionUpdateSendgrind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Functions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ValidateFuntion",
                table: "Functions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidateFuntion",
                table: "Functions");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Functions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
