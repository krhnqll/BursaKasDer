namespace bursaKasder.Models.EntityModels
{
    public class EventViewModel
    {
        public int? ev_ID { get; set; } // Güncelleme için opsiyonel olabilir
        public string ev_Title { get; set; } // Etkinlik başlığı
        public string ev_Content { get; set; } // Etkinlik içeriği
        public DateTime ev_Date { get; set; } // Etkinlik tarihi
        public int ev_Status { get; set; } // Aktif/Pasif gibi durumlar

        // Ana fotoğraf (isteğe bağlı, güncelleme için kullanılabilir)
        public string ev_MainPhoto { get; set; }

        // Fotoğraf yükleme için dosya
        public IFormFile MainPhotoFile { get; set; }

        // Ekstra fotoğraflar listesi (veritabanından gelen URL'ler)
        public List<string> EventPhotos { get; set; }

        // Yeni yüklenecek fotoğraflar
        public List<IFormFile> NewPhotos { get; set; }
    }
}
