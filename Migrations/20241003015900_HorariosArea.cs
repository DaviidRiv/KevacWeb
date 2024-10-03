using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class HorariosArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Horario",
                table: "AreaModel",
                newName: "HorarioI");

            migrationBuilder.AddColumn<string>(
                name: "HorarioF",
                table: "AreaModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorarioF",
                table: "AreaModel");

            migrationBuilder.RenameColumn(
                name: "HorarioI",
                table: "AreaModel",
                newName: "Horario");
        }
    }
}
