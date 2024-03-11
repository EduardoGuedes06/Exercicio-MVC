using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewCentury.Data.Migrations
{
    /// <inheritdoc />
    public partial class Testing_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Jogador",
                table: "Rodada",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jogador",
                table: "Rodada");
        }
    }
}
