using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class CRUDCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteModel",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreRS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreContacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameLastEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastEdit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteModel", x => x.IdCliente);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteModel");
        }
    }
}
