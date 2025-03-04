using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "con_EmailSecond",
                table: "BKD_Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "con_PhoneSecond",
                table: "BKD_Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "con_EmailSecond",
                table: "BKD_Contact");

            migrationBuilder.DropColumn(
                name: "con_PhoneSecond",
                table: "BKD_Contact");
        }
    }
}
