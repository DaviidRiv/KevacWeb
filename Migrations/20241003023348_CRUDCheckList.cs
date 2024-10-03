using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class CRUDCheckList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckListModel",
                columns: table => new
                {
                    IdCheckList = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firma = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListModel", x => x.IdCheckList);
                    table.ForeignKey(
                        name: "FK_CheckListModel_ClienteModel_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "ClienteModel",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_CheckListModel_UsuarioModel_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UsuarioModel",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "CheckListAreaModel",
                columns: table => new
                {
                    IdCheckListArea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    Completada = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListAreaModel", x => x.IdCheckListArea);
                    table.ForeignKey(
                        name: "FK_CheckListAreaModel_AreaModel_AreaId",
                        column: x => x.AreaId,
                        principalTable: "AreaModel",
                        principalColumn: "IdArea");
                    table.ForeignKey(
                        name: "FK_CheckListAreaModel_CheckListModel_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "CheckListModel",
                        principalColumn: "IdCheckList");
                });

            migrationBuilder.CreateTable(
                name: "CheckListTareaModel",
                columns: table => new
                {
                    IdCheckListTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListId = table.Column<int>(type: "int", nullable: true),
                    TareaId = table.Column<int>(type: "int", nullable: true),
                    Completada = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListTareaModel", x => x.IdCheckListTarea);
                    table.ForeignKey(
                        name: "FK_CheckListTareaModel_CheckListModel_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "CheckListModel",
                        principalColumn: "IdCheckList");
                    table.ForeignKey(
                        name: "FK_CheckListTareaModel_TareaModel_TareaId",
                        column: x => x.TareaId,
                        principalTable: "TareaModel",
                        principalColumn: "IdTarea");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListAreaModel_AreaId",
                table: "CheckListAreaModel",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListAreaModel_CheckListId",
                table: "CheckListAreaModel",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListModel_ClienteId",
                table: "CheckListModel",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListModel_UsuarioId",
                table: "CheckListModel",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListTareaModel_CheckListId",
                table: "CheckListTareaModel",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListTareaModel_TareaId",
                table: "CheckListTareaModel",
                column: "TareaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListAreaModel");

            migrationBuilder.DropTable(
                name: "CheckListTareaModel");

            migrationBuilder.DropTable(
                name: "CheckListModel");
        }
    }
}
