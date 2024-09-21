using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRespostaFromCurtida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curtidas_Perguntas_PerguntaId",
                table: "Curtidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Curtidas_Respostas_RespostaId",
                table: "Curtidas");

            migrationBuilder.DropIndex(
                name: "IX_Curtidas_RespostaId",
                table: "Curtidas");

            migrationBuilder.DropColumn(
                name: "RespostaId",
                table: "Curtidas");

            migrationBuilder.AlterColumn<int>(
                name: "PerguntaId",
                table: "Curtidas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Curtidas_Perguntas_PerguntaId",
                table: "Curtidas",
                column: "PerguntaId",
                principalTable: "Perguntas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curtidas_Perguntas_PerguntaId",
                table: "Curtidas");

            migrationBuilder.AlterColumn<int>(
                name: "PerguntaId",
                table: "Curtidas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RespostaId",
                table: "Curtidas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_RespostaId",
                table: "Curtidas",
                column: "RespostaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curtidas_Perguntas_PerguntaId",
                table: "Curtidas",
                column: "PerguntaId",
                principalTable: "Perguntas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Curtidas_Respostas_RespostaId",
                table: "Curtidas",
                column: "RespostaId",
                principalTable: "Respostas",
                principalColumn: "ID");
        }
    }
}
