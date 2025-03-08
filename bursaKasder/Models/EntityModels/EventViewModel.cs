using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models.EntityModels
{
    public class EventViewModel
    {
        public int? ev_ID { get; set; }
        public string ev_Title { get; set; }
        public string ev_Content { get; set; }
        public DateTime ev_Date { get; set; }
        public int ev_Status { get; set; }

        public string ev_MainPhoto { get; set; }

        public IFormFile MainPhotoFile { get; set; }

        public List<string> EventPhotos { get; set; } = new List<string>();

        [NotMapped]
        public List<IFormFile> NewPhotos { get; set; } = new List<IFormFile>();
    }


}

