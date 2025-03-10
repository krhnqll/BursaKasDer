using bursaKasder.HelperClasses;
using Microsoft.EntityFrameworkCore;
using bursaKasder.Models;

namespace bursaKasder.Services
{
    public class Get_AdminService
    {
        private readonly DbContextManager _context;

        public Get_AdminService(DbContextManager context)
        {
            _context = context;
        }

        public async Task<List<BKD_Admins>> GetAllUsers()
        {
            return await _context.BKD_Admins.AsNoTracking().Where(adm => adm.adm_Status == 0 ).OrderByDescending(e => e.adm_ID).ToListAsync();
        }

        public async Task<BKD_Admins?> Get_UserById(int? id_User)
        {
            return id_User == null ? null : await _context.BKD_Admins.FindAsync(id_User);
        }

        public async Task<List<BKD_Announcements>> Get_AllAnnouncements()
        {
            return await _context.BKD_Announcements.AsNoTracking().ToListAsync();
        }

        public async Task<BKD_Announcements?> Get_AnnouncementsById(int? id_Announcements)
        {
            return id_Announcements == null ? null : await _context.BKD_Announcements.FindAsync(id_Announcements);
        }

        public async Task<List<BKD_About>> Get_AllAboutInformation()
        {
            return await _context.BKD_About.AsNoTracking().ToListAsync();
        }

        public async Task<List<BKD_Contact>> Get_AllContactInformation()
        {
            return await _context.BKD_Contact.AsNoTracking().ToListAsync();
        }

        public async Task<List<BKD_ContactUs>> Get_AllContactUs()
        {
            return await _context.BKD_ContactUs.AsNoTracking().ToListAsync();
        }

        public async Task<BKD_ContactUs?> Get_ContactUsById(int? id_ContactUs)
        {
            return id_ContactUs == null ? null : await _context.BKD_ContactUs.FindAsync(id_ContactUs);
        }

        public async Task<List<BKD_Events>> Get_AllEvents()
        {
            return await _context.BKD_Events.AsNoTracking().ToListAsync();
        }

        public async Task<BKD_Events?> Get_EventById(int? id_Event)
        {
            return id_Event == null ? null : await _context.BKD_Events
                .Include(e => e.EventPhotos)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ev_ID == id_Event);
        }

        public async Task<List<BKD_NewsFromKast>> Get_AllNewsFromKast()
        {
            return await _context.BKD_NewsFromKast.AsNoTracking().ToListAsync();
        }

        public async Task<List<BKD_NewsFromUs>> Get_AllNewsFromUs()
        {
            return await _context.BKD_NewsFromUs.AsNoTracking().ToListAsync();
        }

        public async Task<BKD_NewsFromUs?> Get_NewsFromUsById(int? id_NewsFromUs)
        {
            return id_NewsFromUs == null ? null : await _context.BKD_NewsFromUs.FindAsync(id_NewsFromUs);
        }

        public async Task<List<BKD_OrganizationalStructure>> Get_AllOrgStructure()
        {
            return await _context.BKD_OrganizationalStructure.AsNoTracking().ToListAsync();
        }

        public async Task<List<BKD_OrganizationInformation>> Get_AllOrgInfo()
        {
            return await _context.BKD_OrganizationInformation.AsNoTracking().ToListAsync();
        }
    }
}
