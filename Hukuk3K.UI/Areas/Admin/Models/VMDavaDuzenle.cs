using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hukuk3K.UI.Areas.Admin.Models
{
    public class VMDavaDuzenle
    {
        [Required(ErrorMessage = "Dava ID Boş Geçilemez.")]
        public Guid DavaId { get; set; }

        [Required(ErrorMessage = "Dosya No Alanı Boş Geçilemez"), MaxLength(50, ErrorMessage = "50 Karakterden Fazla Girilemez."), MinLength(5, ErrorMessage = "5 Karakterden Az Girilemez.")]
        public string DosyaNo { get; set; }

        [Required(ErrorMessage = "Lütfen Birim Seçiniz")]
        public Guid BirimDaireId { get; set; }

        [Required(ErrorMessage = "Lütfen Durum Seçiniz")]
        public Guid DavaDurumId { get; set; }

        [Required(ErrorMessage = "Lütfen Açılış Tarihi Seçiniz")]
        public DateTime AcilisTarihi { get; set; }

        [Required(ErrorMessage = "TC Kimlik Alanı Boş Geçilemez"), MaxLength(11, ErrorMessage = "11 Karakterden Fazla Girilemez."), MinLength(11, ErrorMessage = "11 Karakterden Az Girilemez.")]
        public string TCKimlikNo { get; set; }
    }
}