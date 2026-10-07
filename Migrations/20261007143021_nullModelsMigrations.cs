using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBank_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class nullModelsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Parcelas_ParcelaId",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioId",
                table: "Transacoes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Transacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParcelaId",
                table: "Transacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Parcelas_ParcelaId",
                table: "Transacoes",
                column: "ParcelaId",
                principalTable: "Parcelas",
                principalColumn: "ParcelaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Parcelas_ParcelaId",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioId",
                table: "Transacoes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Transacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParcelaId",
                table: "Transacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Parcelas_ParcelaId",
                table: "Transacoes",
                column: "ParcelaId",
                principalTable: "Parcelas",
                principalColumn: "ParcelaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
