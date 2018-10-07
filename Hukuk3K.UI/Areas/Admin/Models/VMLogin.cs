using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hukuk3K.UI.Areas.Admin.Models
{
    public class VMLogin
    {
        [Required(ErrorMessage = "Boş Geçilemez"), MinLength(11, ErrorMessage = "TC Kimlik Numarası 11 Haneli Olmalı"), MaxLength(11, ErrorMessage = "TC Kimlik Numarası 11 Haneli Olmalı")]
        public string TCKimlikNo { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez"), MinLength(5, ErrorMessage = "Şifre 5 Karakterden Az Girilemez")]
        public string Sifre { get; set; }
    }
}