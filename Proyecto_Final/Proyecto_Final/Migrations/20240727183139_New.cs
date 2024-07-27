using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Name",
                table: "Servicios",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PhoneNumber",
                table: "Clientes",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Servicios_Name",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_PhoneNumber",
                table: "Clientes");
        }
    }
}
