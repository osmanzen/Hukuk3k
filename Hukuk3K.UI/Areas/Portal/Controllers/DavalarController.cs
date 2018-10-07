using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using Hukuk3K.UI.Areas.Portal.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Areas.Portal.Controllers
{
    [PortalLoginControl]
    public class DavalarController : Controller
    {
        IDavaDAL _davaDAL;
        IDavaDosyaDAL _davaDosyaDAL;
        public DavalarController(IDavaDAL davaDAL, IDavaDosyaDAL davaDosyaDAL)
        {
            _davaDAL = davaDAL;
            _davaDosyaDAL = davaDosyaDAL;
        }

        // GET: Portal/Dava
        public ActionResult Liste()
        {
            string tcKimlik = Session["portal"].ToString();
            List<Dava> davalar = _davaDAL.GetList(x => x.TCKimlikNo == tcKimlik && x.AktifMi == true).ToList();
            return View(davalar);
        }

        public ActionResult Goruntule(Guid id)
        {
            DavaBaslik davaBaslik = new DavaBaslik();
            DavaDosya davaDosyalari = new DavaDosya();
            Dava dava = _davaDAL.Get(x => x.DavaId == id && x.AktifMi == true);
            Dava goruntule = new Dava()
            {
                AcilisTarihi = dava.AcilisTarihi,
                AktifMi = dava.AktifMi,
                BirimDaireId = dava.BirimDaireId,
                BirimDaire = dava.BirimDaire,
                DavaDurum = dava.DavaDurum,
                DavaDurumId = dava.DavaDurumId,
                DavaId = dava.DavaId,
                DosyaNo = dava.DosyaNo,
                TCKimlikNo = dava.TCKimlikNo,
                Kullanici = dava.Kullanici
            };

            foreach (var baslik in dava.DavaBasliklari.Where(x => x.AktifMi == true))
            {
                davaBaslik.AktifMi = baslik.AktifMi;
                davaBaslik.Dava = baslik.Dava;
                davaBaslik.DavaBaslikAdi = baslik.DavaBaslikAdi;
                davaBaslik.DavaBaslikId = baslik.DavaBaslikId;
                davaBaslik.DavaId = baslik.DavaId;
                foreach (var dosya in baslik.DavaDosyalari.Where(x => x.AktifMi == true).OrderByDescending(x => x.EklemeTarihi))
                {
                    davaDosyalari.AktifMi = dosya.AktifMi;
                    davaDosyalari.DavaBaslik = dosya.DavaBaslik;
                    davaDosyalari.DavaBaslikId = dosya.DavaBaslikId;
                    davaDosyalari.DavaDosyaAdi = dosya.DavaDosyaAdi;
                    davaDosyalari.DavaDosyaId = dosya.DavaDosyaId;
                    davaDosyalari.Url = dosya.Url;
                    davaDosyalari.EklemeTarihi = dosya.EklemeTarihi;

                    davaBaslik.DavaDosyalari.Add(davaDosyalari);
                    davaDosyalari = new DavaDosya();
                }
                goruntule.DavaBasliklari.Add(davaBaslik);
                davaBaslik = new DavaBaslik();
            }

            return View(goruntule);
        }

        public ActionResult Evraklar(Guid id)
        {
            List<DavaDosya> dosyalar = _davaDosyaDAL.GetList(x => x.DavaBaslikId == id && x.AktifMi == true).OrderByDescending(x => x.EklemeTarihi).ToList();
            return View(dosyalar);
        }

        public ActionResult Evrak(Guid id)
        {
            DavaDosya dosya = _davaDosyaDAL.Get(x => x.DavaDosyaId == id && x.AktifMi == true);
            return View(dosya);
        }
    }
}