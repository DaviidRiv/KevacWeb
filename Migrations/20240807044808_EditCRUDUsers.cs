using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class EditCRUDUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateLastEdit",
                table: "UsuarioModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameLastEdit",
                table: "UsuarioModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLastEdit",
                table: "UsuarioModel");

            migrationBuilder.DropColumn(
                name: "NameLastEdit",
                table: "UsuarioModel");
        }
    }
}
