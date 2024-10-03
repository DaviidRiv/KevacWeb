using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class CRUDTarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TareaModel",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aux = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameLastEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastEdit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaModel", x => x.IdTarea);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareaModel");
        }
    }
}
