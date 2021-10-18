using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotTR.DataCollector.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Codes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Project = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Name = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Content = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Codes");
        }
    }
}
