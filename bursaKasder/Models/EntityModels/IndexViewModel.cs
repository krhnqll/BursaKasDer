using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class IndexViewModel
    {
        // haberler 


        public List<BKD_OrganizationalStructure>? ListOS {  get; set; }    
        public List<BKD_NewsFromUs>? ListNews {  get; set; }
        public List<BKD_Announcements>? ListAnnouncements { get; set; }
        public List<BKD_Events>? ListEvents { get; set; }


        public BKD_OrganizationInformation? DataOI { get; set; }
        public BKD_About? DataAbout { get; set; }
       public BKD_Contact? DataContact { get; set; }
        



    }
}
