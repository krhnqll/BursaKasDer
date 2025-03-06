﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bursaKasder.HelperClasses;

#nullable disable

namespace bursaKasder.Migrations
{
    [DbContext(typeof(DbContextManager))]
    [Migration("20250305163939_503")]
    partial class _503
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("bursaKasder.Models.BKD_About", b =>
                {
                    b.Property<int>("ab_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ab_ID"));

                    b.Property<string>("ab_History")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ab_HistoryPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ab_MisVisPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ab_Mission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ab_Status")
                        .HasColumnType("int");

                    b.Property<string>("ab_Vision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ab_ID");

                    b.ToTable("BKD_About");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_Admins", b =>
                {
                    b.Property<int>("adm_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("adm_ID"));

                    b.Property<string>("adm_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adm_Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("adm_Status")
                        .HasColumnType("int");

                    b.Property<string>("adm_Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adm_Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("adm_ID");

                    b.ToTable("BKD_Admins");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_Announcements", b =>
                {
                    b.Property<int>("ann_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ann_ID"));

                    b.Property<string>("ann_Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ann_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ann_Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ann_Status")
                        .HasColumnType("int");

                    b.Property<string>("ann_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ann_ID");

                    b.ToTable("BKD_Announcements");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_Contact", b =>
                {
                    b.Property<int>("con_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("con_ID"));

                    b.Property<string>("con_Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_EmailSecond")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_PhoneSecond")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("con_Status")
                        .HasColumnType("int");

                    b.Property<string>("con_URLFacebook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_URLInstagram")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_URLX")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("con_URLYoutube")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("con_ID");

                    b.ToTable("BKD_Contact");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_ContactUs", b =>
                {
                    b.Property<int>("conU_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("conU_ID"));

                    b.Property<DateTime>("conU_DateMessage")
                        .HasColumnType("datetime2");

                    b.Property<string>("conU_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("conU_Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("conU_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("conU_Status")
                        .HasColumnType("int");

                    b.Property<string>("conU_Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("conU_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("conU_ID");

                    b.ToTable("BKD_ContactUs");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_EventPhotos", b =>
                {
                    b.Property<int>("evP_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("evP_ID"));

                    b.Property<int>("evP_EventId")
                        .HasColumnType("int");

                    b.Property<string>("evP_Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("evP_Status")
                        .HasColumnType("int");

                    b.HasKey("evP_ID");

                    b.HasIndex("evP_EventId");

                    b.ToTable("BKD_EventPhotos");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_Events", b =>
                {
                    b.Property<int>("ev_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ev_ID"));

                    b.Property<string>("ev_Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ev_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ev_MainPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ev_Status")
                        .HasColumnType("int");

                    b.Property<string>("ev_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ev_ID");

                    b.ToTable("BKD_Events");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_NewsFromKast", b =>
                {
                    b.Property<int>("newsK_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("newsK_Id"));

                    b.Property<DateTime>("newsK_AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("newsK_Status")
                        .HasColumnType("int");

                    b.Property<string>("newsK_URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("newsK_Id");

                    b.ToTable("BKD_NewsFromKast");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_NewsFromUs", b =>
                {
                    b.Property<int>("newsU_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("newsU_ID"));

                    b.Property<string>("newsU_Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("newsU_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("newsU_Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("newsU_Status")
                        .HasColumnType("int");

                    b.Property<string>("newsU_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("newsU_ID");

                    b.ToTable("BKD_NewsFromUs");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_OrganizationInformation", b =>
                {
                    b.Property<int>("OI_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OI_ID"));

                    b.Property<string>("OI_Indexphoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OI_Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OI_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OI_StatuePhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OI_Status")
                        .HasColumnType("int");

                    b.HasKey("OI_ID");

                    b.ToTable("BKD_OrganizationInformation");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_OrganizationalStructure", b =>
                {
                    b.Property<int>("OS_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OS_ID"));

                    b.Property<string>("OS_Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS_Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS_Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OS_Status")
                        .HasColumnType("int");

                    b.Property<string>("OS_Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OS_ID");

                    b.ToTable("BKD_OrganizationalStructure");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_EventPhotos", b =>
                {
                    b.HasOne("bursaKasder.Models.BKD_Events", "events")
                        .WithMany("EventPhotos")
                        .HasForeignKey("evP_EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("events");
                });

            modelBuilder.Entity("bursaKasder.Models.BKD_Events", b =>
                {
                    b.Navigation("EventPhotos");
                });
#pragma warning restore 612, 618
        }
    }
}
