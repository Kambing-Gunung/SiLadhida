using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiLadhida.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNamaKue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamaKue",
                table: "Pesanan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NamaKue",
                table: "Pesanan",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
