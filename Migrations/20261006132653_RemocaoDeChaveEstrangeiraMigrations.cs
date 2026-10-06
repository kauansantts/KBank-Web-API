using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBank_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoDeChaveEstrangeiraMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprasParceladas_Transacoes_TransacaoId",
                table: "ComprasParceladas");

            migrationBuilder.DropIndex(
                name: "IX_ComprasParceladas_TransacaoId",
                table: "ComprasParceladas");

            migrationBuilder.DropColumn(
                name: "TransacaoId",
                table: "ComprasParceladas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransacaoId",
                table: "ComprasParceladas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComprasParceladas_TransacaoId",
                table: "ComprasParceladas",
                column: "TransacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasParceladas_Transacoes_TransacaoId",
                table: "ComprasParceladas",
                column: "TransacaoId",
                principalTable: "Transacoes",
                principalColumn: "TransacaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
