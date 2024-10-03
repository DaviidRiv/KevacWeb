using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuevakWeb.Migrations
{
    public partial class EditsChecklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateLastEdit",
                table: "CheckListModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameLastEdit",
                table: "CheckListModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLastEdit",
                table: "CheckListModel");

            migrationBuilder.DropColumn(
                name: "NameLastEdit",
                table: "CheckListModel");
        }
    }
}
