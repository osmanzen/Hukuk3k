using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hukuk3K.UI.Areas.Admin.Models
{
    public class VMMuvekkilEkle
    {
        [Required(ErrorMessage = "TC Kimlik Alanı Boş Geçilemez"), MaxLength(11, ErrorMessage = "11 Karakterden Fazla Girilemez."), MinLength(11, ErrorMessage = "11 Karakterden Az Girilemez.")]
        public string TCKimlikNo { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Adres { get; set; }
    }
}