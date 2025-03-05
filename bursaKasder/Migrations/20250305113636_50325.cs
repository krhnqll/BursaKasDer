using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class _50325 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_BKD_EventPhotos_evP_EventId",
                table: "BKD_EventPhotos",
                column: "evP_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_evP_EventId",
                table: "BKD_EventPhotos",
                column: "evP_EventId",
                principalTable: "BKD_Events",
                principalColumn: "ev_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_evP_EventId",
                table: "BKD_EventPhotos");

            migrationBuilder.DropIndex(
                name: "IX_BKD_EventPhotos_evP_EventId",
                table: "BKD_EventPhotos");

            migrationBuilder.AddColumn<int>(
                name: "Eventev_ID",
                table: "BKD_EventPhotos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BKD_EventPhotos_Eventev_ID",
                table: "BKD_EventPhotos",
                column: "Eventev_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos",
                column: "Eventev_ID",
                principalTable: "BKD_Events",
                principalColumn: "ev_ID");
        }
    }
}
