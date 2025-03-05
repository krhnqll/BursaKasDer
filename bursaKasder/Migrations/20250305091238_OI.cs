using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class OI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OI_Indexphoto",
                table: "BKD_OrganizationInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OI_Indexphoto",
                table: "BKD_OrganizationInformation");
        }
    }
}
