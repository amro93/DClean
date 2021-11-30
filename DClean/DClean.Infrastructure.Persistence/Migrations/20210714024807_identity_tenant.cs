using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DClean.Infrastructure.Persistence.Migrations
{
    public partial class identity_tenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AspNetUserClaims",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUserClaims");
        }
    }
}
