using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using Hukuk3K.UI.Areas.Portal.Models.Attributes;

namespace Hukuk3K.UI.Areas.Portal.Controllers
{
    [PortalLoginControl]
    public class HomeController : Controller
    {
        IKullaniciDAL _kullaniciDAL;

        public HomeController(IKullaniciDAL kullaniciDAL)
        {
            _kullaniciDAL = kullaniciDAL;
        }

        // GET: Portal/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}