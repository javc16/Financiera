using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financiera.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_TipoMovimiento_TipoMovimientoId",
                table: "Movimiento");

            migrationBuilder.DropTable(
                name: "TipoMovimiento");

            migrationBuilder.DropIndex(
                name: "IX_Movimiento_TipoMovimientoId",
                table: "Movimiento");

            migrationBuilder.DropColumn(
                name: "TipoMovimientoId",
                table: "Movimiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoMovimientoId",
                table: "Movimiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimiento", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_TipoMovimientoId",
                table: "Movimiento",
                column: "TipoMovimientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_TipoMovimiento_TipoMovimientoId",
                table: "Movimiento",
                column: "TipoMovimientoId",
                principalTable: "TipoMovimiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
