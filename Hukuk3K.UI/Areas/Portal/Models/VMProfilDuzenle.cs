using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hukuk3K.UI.Areas.Portal.Models
{
    public class VMProfilDuzenle
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez."), MinLength(5, ErrorMessage = "Şifre 5 Karakterden Az Girilemez.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez."), MinLength(5, ErrorMessage = "Şifre 5 Karakterden Az Girilemez.")]
        public string SifreTekrar { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Adres { get; set; }
    }
}