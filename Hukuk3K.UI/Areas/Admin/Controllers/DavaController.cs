using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using Hukuk3K.UI.Areas.Admin.Models;
using Hukuk3K.UI.Areas.Admin.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Areas.Admin.Controllers
{
    [LoginControl]
    public class DavaController : Controller
    {
        IDavaDAL _davaDAL;
        ISehirDAL _sehirDAL;
        IAdliyeDAL _adliyeDAL;
        IDavaTipiDAL _davaTipiDAL;
        IDavaDurumDAL _davaDurumDAL;
        IBirimDaireDAL _birimDaireDAL;
        IDavaBaslikDAL _davaBaslikDAL;
        IDavaDosyaDAL _davaDosyaDAL;
        IKullaniciDAL _kullaniciDAL;
        public DavaController(
            IDavaDAL davaDAL,
            ISehirDAL sehirDAL,
            IAdliyeDAL adliyeDAL,
            IDavaTipiDAL davaTipi,
            IDavaDurumDAL davaDurumDAL,
            IBirimDaireDAL birimDaireDAL,
            IDavaBaslikDAL davaBaslikDAL,
            IDavaDosyaDAL davaDosyaDAL,
            IKullaniciDAL kullaniciDAL
            )
        {
            _davaDAL = davaDAL;
            _sehirDAL = sehirDAL;
            _adliyeDAL = adliyeDAL;
            _davaTipiDAL = davaTipi;
            _davaDurumDAL = davaDurumDAL;
            _birimDaireDAL = birimDaireDAL;
            _davaBaslikDAL = davaBaslikDAL;
            _davaDosyaDAL = davaDosyaDAL;
            _kullaniciDAL = kullaniciDAL;
        }

        // GET: Admin/Dava
        public ActionResult Ekle()
        {
            return View(new VMDavaEkle());
        }

        [HttpPost]
        public ActionResult Ekle(VMDavaEkle gelenDava)
        {
            if (ModelState.IsValid)
            {
                if (_davaDAL.Get(x => x.AktifMi == true && x.DosyaNo == gelenDava.DosyaNo && x.BirimDaireId == gelenDava.BirimDaireId) != null)
                {
                    TempData["hata"] = "Seçilen Birimde Girilen Dava Numarası Mevcut.";
                    return View();
                }
                else
                {
                    Dava yeniDava = new Dava()
                    {
                        DavaId = Guid.NewGuid(),
                        AcilisTarihi = gelenDava.AcilisTarihi,
                        BirimDaireId = gelenDava.BirimDaireId,
                        DavaDurumId = gelenDava.DavaDurumId,
                        TCKimlikNo = gelenDava.TCKimlikNo,
                        DosyaNo = gelenDava.DosyaNo,
                        AktifMi = true
                    };

                    _davaDAL.Add(yeniDava);
                    TempData["basarili"] = yeniDava.DosyaNo + " Numaralı Dava Başarıyla Eklendi";
                    return RedirectToAction("Listele");
                }
            }
            else
            {
                TempData["hata"] = "Hatalı İşlem Yaptınız Lütfen Tekrar Deneyiniz.";
                return View();
            }
        }

        [HttpPost]
        public JsonResult SehirGetir()
        {
            var sehirler = _sehirDAL.GetList(x => x.AktifMi == true).Select(x => new { SehirId = x.SehirId, SehirAdi = x.SehirAdi }).OrderBy(x => x.SehirAdi).ToList();
            return Json(sehirler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DurumGetir()
        {
            var durumlar = _davaDurumDAL.GetList(x => x.AktifMi == true).Select(x => new { DavaDurumId = x.DavaDurumId, DavaDurumAdi = x.DavaDurumAdi }).OrderBy(x => x.DavaDurumAdi).ToList();
            return Json(durumlar, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SehirEkle(string sehirAdi)
        {
            Sehir yeniSehir = new Sehir()
            {
                SehirId = Guid.NewGuid(),
                SehirAdi = sehirAdi,
                AktifMi = true,
            };

            _sehirDAL.Add(yeniSehir);

            return Json(new { yeniSehir.SehirId, yeniSehir.SehirAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SehirGuncelle(string sehirAdi, Guid sehirId)
        {
            Sehir sehir = _sehirDAL.Get(x => x.SehirId == sehirId && x.AktifMi == true);
            sehir.SehirAdi = sehirAdi;
            _sehirDAL.Update(sehir);

            return Json(new { sehir.SehirId, sehir.SehirAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SehirSil(Guid sehirId)
        {
            Sehir sehir = _sehirDAL.Get(x => x.SehirId == sehirId && x.AktifMi == true);
            sehir.AktifMi = false;

            _sehirDAL.Update(sehir);

            return Json(new { sehir.SehirId, sehir.SehirAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DavaDurumuEkle(string davaDurumu)
        {
            DavaDurum yeniDurum = new DavaDurum()
            {
                DavaDurumId = Guid.NewGuid(),
                DavaDurumAdi = davaDurumu,
                AktifMi = true
            };

            _davaDurumDAL.Add(yeniDurum);

            return Json(new { yeniDurum.DavaDurumId, yeniDurum.DavaDurumAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DavaDurumGuncelle(string durumAdi, Guid durumId)
        {
            DavaDurum durum = _davaDurumDAL.Get(x => x.DavaDurumId == durumId && x.AktifMi == true);
            durum.DavaDurumAdi = durumAdi;
            _davaDurumDAL.Update(durum);

            return Json(new { durum.DavaDurumId, durum.DavaDurumAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DavaDurumSil(Guid durumId)
        {
            DavaDurum durum = _davaDurumDAL.Get(x => x.DavaDurumId == durumId && x.AktifMi == true);
            durum.AktifMi = false;
            _davaDurumDAL.Update(durum);

            return Json(new { durum.DavaDurumId, durum.DavaDurumAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AdliyeEkle(string adliyeAdi, Guid sehirId)
        {
            Adliye yeniAdliye = new Adliye()
            {
                AdliyeId = Guid.NewGuid(),
                AdliyeAdi = adliyeAdi,
                SehirId = sehirId,
                AktifMi = true
            };

            _adliyeDAL.Add(yeniAdliye);

            return Json(new { yeniAdliye.AdliyeId, yeniAdliye.AdliyeAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AdliyeGuncelle(string adliyeAdi, Guid adliyeId)
        {
            Adliye adliye = _adliyeDAL.Get(x => x.AdliyeId == adliyeId && x.AktifMi == true);
            adliye.AdliyeAdi = adliyeAdi;
            _adliyeDAL.Update(adliye);

            return Json(new { adliye.AdliyeId, adliye.AdliyeAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AdliyeSil(Guid adliyeId)
        {
            Adliye adliye = _adliyeDAL.Get(x => x.AdliyeId == adliyeId && x.AktifMi == true);
            adliye.AktifMi = false;
            _adliyeDAL.Update(adliye);

            return Json(new { adliye.AdliyeId, adliye.AdliyeAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DosyaTuruEkle(string tipAdi, Guid adliyeId)
        {
            DavaTipi yeniTip = new DavaTipi()
            {
                DavaTipiId = Guid.NewGuid(),
                DavaTipiAdi = tipAdi,
                AdliyeId = adliyeId,
                AktifMi = true
            };

            _davaTipiDAL.Add(yeniTip);

            return Json(new { yeniTip.DavaTipiId, yeniTip.DavaTipiAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DosyaTuruGuncelle(string tipAdi, Guid tipId)
        {
            DavaTipi tip = _davaTipiDAL.Get(x => x.DavaTipiId == tipId && x.AktifMi == true);
            tip.DavaTipiAdi = tipAdi;
            _davaTipiDAL.Update(tip);

            return Json(new { tip.DavaTipiId, tip.DavaTipiAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DosyaTuruSil(Guid tipId)
        {
            DavaTipi tip = _davaTipiDAL.Get(x => x.DavaTipiId == tipId && x.AktifMi == true);
            tip.AktifMi = false;
            _davaTipiDAL.Update(tip);

            return Json(new { tip.DavaTipiId, tip.DavaTipiAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BirimEkle(string birimAdi, Guid dosyaId)
        {
            BirimDaire yeniBirim = new BirimDaire()
            {
                BirimDaireId = Guid.NewGuid(),
                BirimDaireAdi = birimAdi,
                DavaTipiId = dosyaId,
                AktifMi = true
            };

            _birimDaireDAL.Add(yeniBirim);

            return Json(new { yeniBirim.BirimDaireId, yeniBirim.BirimDaireAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BirimGuncelle(string birimAdi, Guid birimId)
        {
            BirimDaire birim = _birimDaireDAL.Get(x => x.BirimDaireId == birimId && x.AktifMi == true);
            birim.BirimDaireAdi = birimAdi;
            _birimDaireDAL.Update(birim);

            return Json(new { birim.BirimDaireId, birim.BirimDaireAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BirimSil(Guid birimId)
        {
            BirimDaire birim = _birimDaireDAL.Get(x => x.BirimDaireId == birimId && x.AktifMi == true);
            birim.AktifMi = false;
            _birimDaireDAL.Update(birim);

            return Json(new { birim.BirimDaireId, birim.BirimDaireAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AdliyeGetir(Guid id)
        {
            var adliyeler = _sehirDAL.Get(x => x.AktifMi == true && x.SehirId == id).Adliyeler.Where(x => x.AktifMi == true).Select(x => new { AdliyeId = x.AdliyeId, AdliyeAdi = x.AdliyeAdi }).OrderBy(x => x.AdliyeAdi).ToList();
            return Json(adliyeler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DavaTipiGetir(Guid id)
        {
            var davaTipleri = _adliyeDAL.Get(x => x.AktifMi == true && x.AdliyeId == id).DavaTipleri.Where(x => x.AktifMi == true).Select(x => new { DavaTipiId = x.DavaTipiId, DavaTipiAdi = x.DavaTipiAdi }).OrderBy(x => x.DavaTipiAdi).ToList();
            return Json(davaTipleri, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BirimGetir(Guid id)
        {
            var birimler = _davaTipiDAL.Get(x => x.AktifMi == true && x.DavaTipiId == id).BirimDaireler.Where(x => x.AktifMi == true).Select(x => new { BirimDaireId = x.BirimDaireId, BirimDaireAdi = x.BirimDaireAdi }).OrderBy(x => x.BirimDaireAdi).ToList();
            return Json(birimler, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DavaSil(Guid id)
        {
            var dava = _davaDAL.Get(x => x.DavaId == id);
            dava.AktifMi = false;
            _davaDAL.Update(dava);

            return RedirectToAction("Listele", "Dava");
        }

        public ActionResult Listele()
        {
            return View(_davaDAL.GetList(x => x.AktifMi == true).OrderByDescending(x => x.AcilisTarihi).ToList());
        }

        [HttpPost]
        public ActionResult DavaDetay(Guid davaId)
        {
            var dava = _davaDAL.Get(x => x.AktifMi == true && x.DavaId == davaId);
            if (dava != null)
                return View(dava);
            else
                return RedirectToAction("Listele", "Dava");
        }

        public ActionResult DavaDuzenle(Guid id)
        {
            Dava duzenlenecekDava = _davaDAL.Get(x => x.DavaId == id);
            DuzenleTempData(id);
            return View(duzenlenecekDava);
        }

        private void DuzenleTempData(Guid davaId)
        {
            Dava duzenlenecekDava = _davaDAL.Get(x => x.DavaId == davaId);
            Session["davaId"] = davaId;
            TempData["sehirler"] = _sehirDAL.GetList().OrderBy(x => x.SehirAdi).ToList();
            TempData["adliyeler"] = _adliyeDAL.GetList(x => x.SehirId == duzenlenecekDava.BirimDaire.DavaTipi.Adliye.SehirId).OrderBy(x => x.AdliyeAdi).ToList();
            TempData["dosyalar"] = _davaTipiDAL.GetList(x => x.AdliyeId == duzenlenecekDava.BirimDaire.DavaTipi.AdliyeId).OrderBy(x => x.DavaTipiAdi).ToList();
            TempData["birimler"] = _birimDaireDAL.GetList(x => x.DavaTipiId == duzenlenecekDava.BirimDaire.DavaTipiId).OrderBy(x => x.BirimDaireAdi).ToList();
            TempData["durumlar"] = _davaDurumDAL.GetList().OrderBy(x => x.DavaDurumAdi).ToList();
            TempData["muvekkiller"] = _kullaniciDAL.GetList(x => x.Admin == false).OrderBy(x => x.AdSoyad).ToList();
            List<DavaBaslik> davaBasliklari = _davaBaslikDAL.GetList(x => x.AktifMi == true && x.DavaId == davaId).OrderBy(x => x.DavaBaslikAdi).ToList();
            TempData["basliklar"] = davaBasliklari;
            if (davaBasliklari.Count > 0)
            {
                Guid ilkBaslikId = davaBasliklari.FirstOrDefault().DavaBaslikId;
                TempData["pdfler"] = _davaDosyaDAL.GetList(x => x.DavaBaslikId == ilkBaslikId && x.AktifMi == true).OrderByDescending(x => x.EklemeTarihi).ToList();
            }
            else
                TempData["pdfler"] = null;
        }

        [HttpPost]
        public ActionResult DavaDuzenle(VMDavaDuzenle gelenDava)
        {
            if (ModelState.IsValid)
            {
                Dava davaDegistir = _davaDAL.Get(x => x.DavaId == gelenDava.DavaId);

                davaDegistir.AcilisTarihi = gelenDava.AcilisTarihi;
                davaDegistir.BirimDaireId = gelenDava.BirimDaireId;
                davaDegistir.DavaDurumId = gelenDava.DavaDurumId;
                davaDegistir.TCKimlikNo = gelenDava.TCKimlikNo;
                davaDegistir.DosyaNo = gelenDava.DosyaNo;

                _davaDAL.Update(davaDegistir);
                _davaDAL.Save();

                TempData["basarili"] = davaDegistir.DosyaNo + " Numaralı Dava Başarıyla Güncellendi.";
                return RedirectToAction("Listele");
            }
            else
            {
                DuzenleTempData(gelenDava.DavaId);
                if (gelenDava.AcilisTarihi == (new DateTime(0001, 01, 01)))
                {
                    TempData["hata"] = "Tarih Alanı Boş Geçilemez.";
                }
                else
                {
                    TempData["hata"] = "Hatalı İşlem Yaptınız Lütfen Tekrar Deneyiniz.";
                }
                return RedirectToAction("DavaDuzenle", new { id = gelenDava.DavaId });
            }
        }

        [HttpPost]
        public JsonResult BaslikGetir()
        {
            Guid DavaId = new Guid(Session["davaId"].ToString());
            var basliklar = _davaBaslikDAL.GetList(x => x.AktifMi == true && x.DavaId == DavaId).Select(x => new { DavaId = x.DavaBaslikId, BaslikAdi = x.DavaBaslikAdi }).OrderBy(x => x.BaslikAdi).ToList();
            return Json(basliklar, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BaslikEkle(string baslikAdi)
        {
            DavaBaslik yeniBaslik = new DavaBaslik()
            {
                DavaBaslikId = Guid.NewGuid(),
                DavaBaslikAdi = baslikAdi,
                AktifMi = true,
                DavaId = new Guid(Session["davaId"].ToString()),
            };

            _davaBaslikDAL.Add(yeniBaslik);

            return Json(new { yeniBaslik.DavaBaslikId, yeniBaslik.DavaBaslikAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BaslikGuncelle(string baslikAdi, Guid baslikId)
        {
            DavaBaslik baslik = _davaBaslikDAL.Get(x => x.DavaBaslikId == baslikId && x.AktifMi == true);
            baslik.DavaBaslikAdi = baslikAdi;
            _davaBaslikDAL.Update(baslik);

            return Json(new { baslik.DavaBaslikId, baslik.DavaBaslikAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BaslikSil(Guid baslikId)
        {
            DavaBaslik baslik = _davaBaslikDAL.Get(x => x.DavaBaslikId == baslikId && x.AktifMi == true);
            baslik.AktifMi = false;
            _davaBaslikDAL.Update(baslik);

            return Json(new { baslik.DavaBaslikId, baslik.DavaBaslikAdi }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DosyaUpload(HttpPostedFileBase[] files, Guid DavaBaslikId)
        {
            if (ModelState.IsValid)
            {
                int counter = 1;
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
                        var tarih = timeZones.FirstOrDefault(x=>x.Id.ToLower().Contains("turkey"));
                        Guid yeniId = Guid.NewGuid();
                        DavaDosya yeniDosya = new DavaDosya() { AktifMi = true, DavaBaslikId = DavaBaslikId, DavaDosyaAdi = file.FileName.Substring(0, file.FileName.Length - 4), DavaDosyaId = yeniId, Url = yeniId + ".pdf", EklemeTarihi = TimeZoneInfo.ConvertTime(DateTime.Now,tarih) };
                        _davaDosyaDAL.Add(yeniDosya);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Assets/DavaPDF/") + yeniId + ".pdf");
                        file.SaveAs(ServerSavePath);
                        TempData["uploadStatus"] = files.Count().ToString() + " dosya yüklemesi tamamlandı.";

                        counter++;
                    }
                }
            }
            return RedirectToActionPermanent("DavaDuzenle", new { id = Session["davaId"].ToString() });
        }

        [HttpPost]
        public ActionResult DavaPdfGetir(Guid id)
        {
            List<DavaDosya> davaDosyalari = new List<DavaDosya>();
            Guid DavaId = new Guid(Session["davaId"].ToString());
            Dava dava = _davaDAL.Get(x => x.DavaId == DavaId);
            davaDosyalari.AddRange(dava.DavaBasliklari.Where(x => x.DavaBaslikId == id && x.AktifMi == true).FirstOrDefault().DavaDosyalari.Where(x => x.AktifMi == true).OrderByDescending(x => x.EklemeTarihi));

            return PartialView("~/Areas/Admin/Views/Dava/DavaPdfGetir.cshtml", davaDosyalari);
        }

        [HttpPost]
        public JsonResult DavaDosyaSil(Guid davaDosyaId)
        {
            Guid davaId = new Guid(Session["davaId"].ToString());
            DavaDosya silinecekDosya = _davaDosyaDAL.Get(x => x.DavaDosyaId == davaDosyaId);
            silinecekDosya.AktifMi = false;
            _davaDosyaDAL.Update(silinecekDosya);
            return Json(new { silinecekDosya.DavaDosyaId, silinecekDosya.DavaDosyaAdi }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DosyaAdiDegistir(Guid davaDosyaId, string yeniAd)
        {
            Guid davaId = new Guid(Session["davaId"].ToString());
            DavaDosya degistirilecekDosya = _davaDosyaDAL.Get(x => x.DavaDosyaId == davaDosyaId);
            degistirilecekDosya.DavaDosyaAdi = yeniAd;
            _davaDosyaDAL.Update(degistirilecekDosya);
            DuzenleTempData(davaId);

            return Json(new { degistirilecekDosya.DavaDosyaId, degistirilecekDosya.DavaDosyaAdi }, JsonRequestBehavior.AllowGet);
        }
    }
}
