using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class aboutHK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ab_MisVis",
                table: "BKD_About",
                newName: "ab_Vision");

            migrationBuilder.AddColumn<string>(
                name: "ab_Mission",
                table: "BKD_About",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ab_Mission",
                table: "BKD_About");

            migrationBuilder.RenameColumn(
                name: "ab_Vision",
                table: "BKD_About",
                newName: "ab_MisVis");
        }
    }
}
