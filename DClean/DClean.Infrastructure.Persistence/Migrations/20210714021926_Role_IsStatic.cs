using Microsoft.EntityFrameworkCore.Migrations;

namespace DClean.Infrastructure.Persistence.Migrations
{
    public partial class Role_IsStatic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "AspNetRoles");
        }
    }
}
