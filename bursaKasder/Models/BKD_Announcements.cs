﻿using System.ComponentModel.DataAnnotations;

namespace bursaKasder.Models
{
    public class BKD_Announcements
    {

        [Key]
        public int ann_ID { get; set; }

        [Required]
        public string ann_Title { get; set; }

        [Required]
        public string ann_Content { get; set; }

        [Required]
        public string ann_Photo { get; set; }

        [Required]
        public DateTime ann_Date { get; set; }

        [Required]
        public int ann_Status { get; set; }
    }
}
