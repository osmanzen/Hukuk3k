using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using Hukuk3K.UI.Areas.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Areas.Portal.Controllers
{
    public class MuvekkilController : Controller
    {
        IKullaniciDAL _kullaniciDAL;

        public MuvekkilController(IKullaniciDAL kullaniciDAL)
        {
            _kullaniciDAL = kullaniciDAL;
        }

        // GET: Portal/Muvekkil
        public ActionResult Index()
        {
            string tc = Session["portal"].ToString();
            Kullanici muvekkil = _kullaniciDAL.Get(x => x.TCKimlikNo == tc && x.AktifMi == true);
            VMProfilDuzenle profil = new VMProfilDuzenle()
            {
                Adres = muvekkil.Adres,
                EMail = muvekkil.EMail,
                AdSoyad = muvekkil.AdSoyad,
                Sifre = muvekkil.Sifre,
                SifreTekrar = muvekkil.Sifre,
                Tel = muvekkil.Tel
            };

            return View(profil);
        }

        [HttpPost]
        public ActionResult Index(VMProfilDuzenle gelenKullanici)
        {
            if (ModelState.IsValid)
            {
                string tc = Session["portal"].ToString();
                Kullanici muvekkil = _kullaniciDAL.Get(x => x.TCKimlikNo == tc && x.AktifMi == true);

                muvekkil.AdSoyad = gelenKullanici.AdSoyad;
                if (gelenKullanici.Sifre == gelenKullanici.SifreTekrar)
                {
                    muvekkil.EMail = gelenKullanici.EMail;
                }
                else
                {
                    TempData["mesaj"] = "Şifreler Uyuşmuyor.";
                    return View(gelenKullanici);
                }
                muvekkil.Sifre = gelenKullanici.SifreTekrar;
                muvekkil.Tel = gelenKullanici.Tel;
                muvekkil.Adres = gelenKullanici.Adres;

                _kullaniciDAL.Update(muvekkil);
                TempData["basarili"] = "Güncelleme İşlemi Başarılı";

                return View(gelenKullanici);
            }
            else
            {
                TempData["mesaj"] = "Hatalı İşlem Yaptınız. Lütfen Tekrar Deneyiniz.";
                return View(gelenKullanici);
            }
        }
    }
}