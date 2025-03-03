using bursaKasder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace bursaKasder.HelperClasses
{
    public class DbContextManager : DbContext
    {
        public DbContextManager(DbContextOptions<DbContextManager> options)
            : base(options) { }

        public DbSet<BKD_Admins> BKD_Admins { get; set; }
        public DbSet<BKD_Announcements> BKD_Announcements { get; set; }
        public DbSet<BKD_About> BKD_About { get; set; }
        public DbSet<BKD_Contact> BKD_Contact { get; set; }
        public DbSet<BKD_ContactUs> BKD_ContactUs { get; set; }
        public DbSet<BKD_EventPhotos> BKD_EventPhotos { get; set; }
        public DbSet<BKD_Events> BKD_Events { get; set; }
        public DbSet<BKD_NewsFromKast> BKD_NewsFromKast { get; set; }
        public DbSet<BKD_NewsFromUs> BKD_NewsFromUs { get; set; }
        public DbSet<BKD_OrganizationalStructure> BKD_OrganizationalStructure { get; set; }
        public DbSet<BKD_OrganizationInformation> BKD_OrganizationInformation { get; set; }
    }
}
