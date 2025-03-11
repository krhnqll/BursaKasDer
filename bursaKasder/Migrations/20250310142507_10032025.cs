using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class _10032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "BKD_OrganizationInformation",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "BKD_OrganizationInformation");
        }
    }
}
