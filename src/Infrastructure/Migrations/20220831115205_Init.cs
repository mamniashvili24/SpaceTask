using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Watchlist",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                FilmId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Poster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Watchlist", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Watchlist");
    }
}