using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using Hukuk3K.UI.Areas.Admin.Models;
using Hukuk3K.UI.Areas.Admin.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Areas.Admin.Controllers
{
    [LoginControl]
    public class MuvekkilController : Controller
    {
        IKullaniciDAL _kullaniciDAL;
        IDavaDAL _davaDAL;

        public MuvekkilController(IKullaniciDAL kullaniciDAL, IDavaDAL davaDAL)
        {
            _kullaniciDAL = kullaniciDAL;
            _davaDAL = davaDAL;
        }


        // GET: Admin/Muvekkil
        public ActionResult Liste()
        {
            return View(_kullaniciDAL.GetList(x => x.Admin == false));
        }

        public ActionResult Ekle()
        {
            return View(new VMMuvekkilEkle());
        }

        [HttpPost]
        public ActionResult Ekle(VMMuvekkilEkle gelen)
        {
            if (ModelState.IsValid)
            {
                if (_kullaniciDAL.Get(x => x.TCKimlikNo == gelen.TCKimlikNo) == null)
                {
                    Kullanici yeniKullanici = new Kullanici()
                    {
                        Admin = false,
                        Adres = gelen.Adres,
                        AdSoyad = gelen.AdSoyad,
                        EMail = gelen.EMail,
                        Sifre = gelen.TCKimlikNo.Substring(5),
                        TCKimlikNo = gelen.TCKimlikNo,
                        Tel = gelen.Tel,
                        AktifMi = true
                    };

                    _kullaniciDAL.Add(yeniKullanici);

                    return RedirectToActionPermanent("Liste");
                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult AktifGetir()
        {
            var kullanicilar = _kullaniciDAL.GetList(x => x.Admin == false).Select(x => new { TCKimlikNo = x.TCKimlikNo, AdSoyad = x.AdSoyad }).OrderBy(x => x.AdSoyad).ToList();
            return Json(kullanicilar, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Duzenle(string id)
        {
            var muvekkil = _kullaniciDAL.Get(x => x.TCKimlikNo == id && x.Admin == false);
            return View(muvekkil);
        }

        [HttpPost]
        public ActionResult Duzenle(Kullanici gelenKullanici)
        {
            var muvekkil = _kullaniciDAL.Get(x => x.TCKimlikNo == gelenKullanici.TCKimlikNo && x.Admin == false);
            muvekkil.AdSoyad = gelenKullanici.AdSoyad;
            muvekkil.Tel = gelenKullanici.Tel;
            muvekkil.EMail = gelenKullanici.EMail;
            muvekkil.Adres = gelenKullanici.Adres;

            _kullaniciDAL.Update(muvekkil);

            TempData["mesaj"] = "Güncelleme İşlemi Başarılı";

            return RedirectToActionPermanent("Liste");
        }

        public ActionResult Sil(string id)
        {
            Kullanici kullanici = _kullaniciDAL.Get(x => x.TCKimlikNo == id && x.Admin == false);
            kullanici.AktifMi = false;

            _kullaniciDAL.Update(kullanici);

            TempData["mesaj"] = "Silme İşlemi Başarılı";
            return RedirectToActionPermanent("Liste");
        }
    }
}