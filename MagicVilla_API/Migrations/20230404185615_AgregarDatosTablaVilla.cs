using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDatosTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Datalle de la Villa Real", new DateTime(2023, 4, 4, 12, 56, 15, 429, DateTimeKind.Local).AddTicks(7598), new DateTime(2023, 4, 4, 12, 56, 15, 429, DateTimeKind.Local).AddTicks(7584), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "Datalle de la Villa Premium", new DateTime(2023, 4, 4, 12, 56, 15, 429, DateTimeKind.Local).AddTicks(7601), new DateTime(2023, 4, 4, 12, 56, 15, 429, DateTimeKind.Local).AddTicks(7600), "", 40, "Premium Vista a la Piscina", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
