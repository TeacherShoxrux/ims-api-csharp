using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talim_aspnet_core.Migrations
{
    /// <inheritdoc />
    public partial class costmerNamelower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fullNameLower",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fullNameLower",
                table: "Customers");
        }
    }
}
