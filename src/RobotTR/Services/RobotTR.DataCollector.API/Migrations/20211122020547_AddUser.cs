using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotTR.DataCollector.API.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUser",
                table: "Codes",
                type: "varchar(MAX)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUser",
                table: "Codes");
        }
    }
}
