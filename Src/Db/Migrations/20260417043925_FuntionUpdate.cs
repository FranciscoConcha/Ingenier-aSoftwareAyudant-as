using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoDivine.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class FuntionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Functions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Functions");
        }
    }
}
