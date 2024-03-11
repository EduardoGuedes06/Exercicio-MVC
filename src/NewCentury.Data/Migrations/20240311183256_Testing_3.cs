using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewCentury.Data.Migrations
{
    /// <inheritdoc />
    public partial class Testing_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Partida_IdJogador",
                table: "Partida",
                column: "IdJogador");

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_Jogador_IdJogador",
                table: "Partida",
                column: "IdJogador",
                principalTable: "Jogador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partida_Jogador_IdJogador",
                table: "Partida");

            migrationBuilder.DropIndex(
                name: "IX_Partida_IdJogador",
                table: "Partida");
        }
    }
}
