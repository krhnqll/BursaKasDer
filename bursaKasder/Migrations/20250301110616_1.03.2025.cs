using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class _1032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Eventev_ID",
                table: "BKD_EventPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "adm_Status",
                table: "BKD_Admins",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BKD_EventPhotos_Eventev_ID",
                table: "BKD_EventPhotos",
                column: "Eventev_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos",
                column: "Eventev_ID",
                principalTable: "BKD_Events",
                principalColumn: "ev_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos");

            migrationBuilder.DropIndex(
                name: "IX_BKD_EventPhotos_Eventev_ID",
                table: "BKD_EventPhotos");

            migrationBuilder.DropColumn(
                name: "Eventev_ID",
                table: "BKD_EventPhotos");

            migrationBuilder.AlterColumn<string>(
                name: "adm_Status",
                table: "BKD_Admins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
