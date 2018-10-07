using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using Hukuk3K.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Areas.Portal.Controllers
{
    public class LoginController : Controller
    {
        IKullaniciDAL _kullaniciDAL;

        public LoginController(IKullaniciDAL kullaniciDAL)
        {
            _kullaniciDAL = kullaniciDAL;
        }

        // GET: Portal/Login
        public ActionResult Index()
        {
            return View(new VMLogin());
        }

        [HttpPost]
        public ActionResult Index(VMLogin model)
        {
            if (ModelState.IsValid)
            {
                Kullanici user = _kullaniciDAL.Get(x => x.TCKimlikNo == model.TCKimlikNo && x.Sifre == model.Sifre && x.Admin == false);
                if (user != null)
                {
                    Session["portal"] = user.TCKimlikNo;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["hata"] = "TC Kimlik No yada Şifre Hatalı.";
                }
            }
            return View();
        }

        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}