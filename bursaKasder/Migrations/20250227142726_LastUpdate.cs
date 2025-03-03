using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bursaKasder.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BKD_About",
                columns: table => new
                {
                    ab_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ab_History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ab_HistoryPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ab_MisVis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ab_MisVisPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ab_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_About", x => x.ab_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_Admins",
                columns: table => new
                {
                    adm_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adm_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adm_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adm_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adm_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deneme = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_Admins", x => x.adm_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_Announcements",
                columns: table => new
                {
                    ann_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ann_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ann_Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ann_Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ann_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ann_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_Announcements", x => x.ann_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_Contact",
                columns: table => new
                {
                    con_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    con_Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_URLInstagram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_URLFacebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_URLYoutube = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_URLX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    con_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_Contact", x => x.con_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_ContactUs",
                columns: table => new
                {
                    conU_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    conU_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conU_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conU_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conU_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conU_Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conU_DateMessage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    conU_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_ContactUs", x => x.conU_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_EventPhotos",
                columns: table => new
                {
                    evP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    evP_Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    evP_EventId = table.Column<int>(type: "int", nullable: false),
                    evP_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_EventPhotos", x => x.evP_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_Events",
                columns: table => new
                {
                    ev_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ev_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ev_Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ev_MainPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ev_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ev_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_Events", x => x.ev_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_NewsFromKast",
                columns: table => new
                {
                    newsK_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    newsK_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    newsK_AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    newsK_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_NewsFromKast", x => x.newsK_Id);
                });

            migrationBuilder.CreateTable(
                name: "BKD_NewsFromUs",
                columns: table => new
                {
                    newsU_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    newsU_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    newsU_Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    newsU_Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    newsU_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    newsU_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_NewsFromUs", x => x.newsU_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_OrganizationalStructure",
                columns: table => new
                {
                    OS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS_Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS_Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS_Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_OrganizationalStructure", x => x.OS_ID);
                });

            migrationBuilder.CreateTable(
                name: "BKD_OrganizationInformation",
                columns: table => new
                {
                    OI_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OI_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_StatuePhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BKD_OrganizationInformation", x => x.OI_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BKD_About");

            migrationBuilder.DropTable(
                name: "BKD_Admins");

            migrationBuilder.DropTable(
                name: "BKD_Announcements");

            migrationBuilder.DropTable(
                name: "BKD_Contact");

            migrationBuilder.DropTable(
                name: "BKD_ContactUs");

            migrationBuilder.DropTable(
                name: "BKD_EventPhotos");

            migrationBuilder.DropTable(
                name: "BKD_Events");

            migrationBuilder.DropTable(
                name: "BKD_NewsFromKast");

            migrationBuilder.DropTable(
                name: "BKD_NewsFromUs");

            migrationBuilder.DropTable(
                name: "BKD_OrganizationalStructure");

            migrationBuilder.DropTable(
                name: "BKD_OrganizationInformation");
        }
    }
}
