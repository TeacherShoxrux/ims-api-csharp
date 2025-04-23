using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talim_aspnet_core.Migrations
{
    /// <inheritdoc />
    public partial class productChangedSomeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantityAlarms",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantityAlarms",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "unit",
                table: "Products");
        }
    }
}
