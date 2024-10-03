using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class CRUDArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaModel",
                columns: table => new
                {
                    IdArea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aux = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameLastEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastEdit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaModel", x => x.IdArea);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaModel");
        }
    }
}
