using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos");

            migrationBuilder.AlterColumn<string>(
                name: "OS_Surname",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OS_Photo",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OS_Degree",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OS_Comment",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Eventev_ID",
                table: "BKD_EventPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos",
                column: "Eventev_ID",
                principalTable: "BKD_Events",
                principalColumn: "ev_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos");

            migrationBuilder.AlterColumn<string>(
                name: "OS_Surname",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OS_Photo",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OS_Degree",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OS_Comment",
                table: "BKD_OrganizationalStructure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Eventev_ID",
                table: "BKD_EventPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BKD_EventPhotos_BKD_Events_Eventev_ID",
                table: "BKD_EventPhotos",
                column: "Eventev_ID",
                principalTable: "BKD_Events",
                principalColumn: "ev_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
