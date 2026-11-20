using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairIT.CatalogService.Migrations;

/// <inheritdoc />
public partial class _20261120231353_CreatingUserTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId_Keycloak = table.Column<string>(type: "TEXT", nullable: false),
                FirstName = table.Column<string>(type: "TEXT", nullable: true),
                LastName = table.Column<string>(type: "TEXT", nullable: true),
                UserRole = table.Column<int>(type: "INTEGER", nullable: false),
                IsActivated = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
