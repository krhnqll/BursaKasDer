using bursaKasder.HelperClasses;
using bursaKasder.Models;


namespace bursaKasder.Services
{
    public class Post_AdminService
    {
        private readonly DbContextManager _context;
        
        public Post_AdminService(DbContextManager context)
        {
            _context = context;
            
       
        }
        public enum AdminStatus
        {
            Inactive = 0, // Yeni eklenen
            Active = 1,   // Güncellenen
            Deleted = 2   // Silinen
        }
        private void SetStatus<T>(T entity, AdminStatus status) where T : class
        {
            var statusProperty = typeof(T).GetProperty("Status");
            if (statusProperty != null)
            {
                statusProperty.SetValue(entity, (int)status);
            }
        }


        public async Task<bool> Post_admin_add_NewUser(BKD_Admins NewUser)
        {
            SetStatus(NewUser, AdminStatus.Inactive);

            _context.BKD_Admins.Add(NewUser);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }

        public async Task<bool> Post_admin_Edit_Profile(BKD_Admins UpdatedUser)
        {
            var existingUser = await _context.BKD_Admins.FindAsync(UpdatedUser.adm_ID);

            if (existingUser == null)
            {
                return false; 
            }

            existingUser.adm_Name = UpdatedUser.adm_Name;
            existingUser.adm_Surname = UpdatedUser.adm_Surname;
            existingUser.adm_Username = UpdatedUser.adm_Username;
            existingUser.adm_Password = UpdatedUser.adm_Password;
            

            _context.BKD_Admins.Update(existingUser);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }

        public async Task<bool> Post_admin_Delete_User(BKD_Admins DeletedUser)
        {
            var User = await _context.BKD_Admins.FindAsync(DeletedUser.adm_ID);
            if (User == null)
            {
                return false;
            }
            else
            {
                SetStatus(User, AdminStatus.Deleted);
                _context.BKD_Admins.Update(User);

                return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

            }

        }

        //------------------------------------ Edit Operations ---------------------------------------------

        public async Task<bool> Post_admin_Edit_About(BKD_About UpdatedAbout)
        {
            var existingAboutInfo = await _context.BKD_About.FindAsync(UpdatedAbout.ab_ID);

            if (existingAboutInfo == null)
            {
                return false;
            }

            existingAboutInfo.ab_Mission = UpdatedAbout.ab_Mission;
            existingAboutInfo.ab_Vision = UpdatedAbout.ab_Vision;
            existingAboutInfo.ab_MisVisPhoto = UpdatedAbout.ab_MisVisPhoto;
            existingAboutInfo.ab_History = UpdatedAbout.ab_History;
            existingAboutInfo.ab_HistoryPhoto = UpdatedAbout.ab_HistoryPhoto;
            SetStatus(existingAboutInfo, AdminStatus.Active);

            _context.BKD_About.Update(existingAboutInfo);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }

        public async Task<bool> Post_admin_Edit_OrganizationInfo(BKD_OrganizationInformation UpdatedOrgInfo)
        {
            var existingOrganizationInfo = await _context.BKD_OrganizationInformation.FindAsync(UpdatedOrgInfo.OI_ID);

            if (existingOrganizationInfo == null)
            {
                return false;
            }

            existingOrganizationInfo.OI_StatuePhoto = UpdatedOrgInfo.OI_StatuePhoto;
            existingOrganizationInfo.OI_Logo = UpdatedOrgInfo.OI_Logo;
            existingOrganizationInfo.OI_Name = UpdatedOrgInfo.OI_Name;
            SetStatus(existingOrganizationInfo, AdminStatus.Active);

            _context.BKD_OrganizationInformation.Update(existingOrganizationInfo);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }
        public async Task<bool> Post_Edit_Organizational_Struct(BKD_OrganizationalStructure UpdatedOrgStructer)
        {
            var existingOrgStrInfo = await _context.BKD_OrganizationalStructure.FindAsync(UpdatedOrgStructer.OS_ID);

            if (existingOrgStrInfo == null)
            {
                return false;
            }

            existingOrgStrInfo.OS_Surname = UpdatedOrgStructer.OS_Surname;
            existingOrgStrInfo.OS_Name = UpdatedOrgStructer.OS_Name;
            existingOrgStrInfo.OS_Degree = UpdatedOrgStructer.OS_Degree;
            existingOrgStrInfo.OS_Photo = UpdatedOrgStructer.OS_Photo;
            existingOrgStrInfo.OS_Comment = UpdatedOrgStructer.OS_Comment;
            SetStatus(existingOrgStrInfo, AdminStatus.Active);

            _context.BKD_OrganizationalStructure.Update(existingOrgStrInfo);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }
        public async Task<bool> Post_admin_edit_NewsFromUs(BKD_NewsFromUs UpdatedNewsU)
        {
            var existingNewsUInfo = await _context.BKD_NewsFromUs.FindAsync(UpdatedNewsU.newsU_ID);

            if (existingNewsUInfo == null)
            {
                return false;
            }

            existingNewsUInfo.newsU_Title = UpdatedNewsU.newsU_Title;
            existingNewsUInfo.newsU_Content = UpdatedNewsU.newsU_Content;
            existingNewsUInfo.newsU_Photo = UpdatedNewsU.newsU_Photo;
            existingNewsUInfo.newsU_Date = UpdatedNewsU.newsU_Date;
            SetStatus(existingNewsUInfo, AdminStatus.Active);

            _context.BKD_NewsFromUs.Update(existingNewsUInfo);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }
        public async Task<bool> Post_admin_edit_Events(BKD_Events UpdatedEvents)
        {
            var existingEventsInfo = await _context.BKD_Events.FindAsync(UpdatedEvents.ev_ID);

            if (existingEventsInfo == null)
            {
                return false;
            }

            existingEventsInfo.ev_Title = UpdatedEvents.ev_Title;
            existingEventsInfo.ev_Content = UpdatedEvents.ev_Content;
            existingEventsInfo.ev_MainPhoto = UpdatedEvents.ev_MainPhoto;
            existingEventsInfo.ev_Date = UpdatedEvents.ev_Date;
            SetStatus(existingEventsInfo, AdminStatus.Active);

            _context.BKD_Events.Update(existingEventsInfo);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }
        public async Task<bool> Post_admin_edit_Contact(BKD_Contact UpdatedContact)
        {
            var existingContactInfo = await _context.BKD_Contact.FindAsync(UpdatedContact.con_ID);

            if (existingContactInfo == null)
            {
                return false;
            }

            existingContactInfo.con_Phone = UpdatedContact.con_Phone;
            existingContactInfo.con_Adress = UpdatedContact.con_Adress;
            existingContactInfo.con_Email = UpdatedContact.con_Email;
            existingContactInfo.con_URLInstagram = UpdatedContact.con_URLInstagram;
            existingContactInfo.con_URLFacebook = UpdatedContact.con_URLFacebook;
            existingContactInfo.con_URLX = UpdatedContact.con_URLX;
            SetStatus(existingContactInfo, AdminStatus.Active);

            _context.BKD_Contact.Update(existingContactInfo);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }

        public async Task<bool> Post_admin_edit_Announcements(BKD_Announcements UpdatedAnnouncements, IFormFile photoFile)
        {
            var existingAnnoucementsInfo = await _context.BKD_Announcements.FindAsync(UpdatedAnnouncements.ann_ID);

            //if (existingAnnoucementsInfo == null)
            //{
            //    return false;
            //}

            //if (photoFile != null && photoFile.Length > 0)
            //{
            //    string uploadedPhotoPath = await _fileService.UploadFileAsync(photoFile);
            //    existingAnnoucementsInfo.ann_Photo = uploadedPhotoPath; 
            //}

            
            existingAnnoucementsInfo.ann_Title = UpdatedAnnouncements.ann_Title;
            existingAnnoucementsInfo.ann_Content = UpdatedAnnouncements.ann_Content;
            existingAnnoucementsInfo.ann_Date = UpdatedAnnouncements.ann_Date;
            SetStatus(existingAnnoucementsInfo, AdminStatus.Active);

            _context.BKD_Announcements.Update(existingAnnoucementsInfo);

            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");
        }


        //------------------------------------- Add Operations -------------------------------------------

        public async Task<bool> Post_admin_add_Announcements(BKD_Announcements NewAnnouncements)
        {
            SetStatus(NewAnnouncements, AdminStatus.Inactive);

            _context.BKD_Announcements.Add(NewAnnouncements);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }

        // Düzenleme yapılacak, Etkinlikler eklenirken fotoğraflar da eklenecek.
        public async Task<bool> Post_admin_add_Events(BKD_Events NewEvent)
        {
            SetStatus(NewEvent, AdminStatus.Inactive);

            // Eğer fotoğraf listesi boş değilse, ilişkisini ayarla
            if (NewEvent.EventPhotos != null && NewEvent.EventPhotos.Any())
            {
                foreach (var photo in NewEvent.EventPhotos)
                {
                    photo.evP_EventId = NewEvent.ev_ID; // Foreign Key ilişkilendirme
                }
            }

            _context.BKD_Events.Add(NewEvent);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");
        }

        public async Task<bool> Post_admin_add_NewsFromUs(BKD_NewsFromUs NewSu)
        {
            SetStatus(NewSu, AdminStatus.Inactive);

            _context.BKD_NewsFromUs.Add(NewSu);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }
        public async Task<bool> Post_admin_add_OrgStructure(BKD_OrganizationalStructure NewStr)
        {
            SetStatus(NewStr, AdminStatus.Inactive);

            _context.BKD_OrganizationalStructure.Add(NewStr);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Database update failed.");

        }

        public async Task<bool> AddEventWithPhotos(BKD_Events newEvent, List<string> photoPaths)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Etkinliği ekle
                    _context.BKD_Events.Add(newEvent);
                    await _context.SaveChangesAsync();

                    // 2. Etkinlik ID’sini al
                    int eventId = newEvent.ev_ID;

                    // 3. Fotoğrafları ekle
                    foreach (var photoPath in photoPaths)
                    {
                        var eventPhoto = new BKD_EventPhotos
                        {
                            evP_Photo = photoPath,
                            evP_EventId = eventId
                        };
                        _context.BKD_EventPhotos.Add(eventPhoto);
                    }

                    await _context.SaveChangesAsync();

                    // 4. İşlemi tamamla
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    // Hata olursa geri al
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }


    }
}
