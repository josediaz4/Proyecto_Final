using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Servicios",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Servicios",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Servicios",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Duracion",
                table: "Servicios",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Servicios",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Servicios",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Servicios",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Servicios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Servicios",
                newName: "Duracion");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Servicios",
                newName: "Descripcion");
        }
    }
}
