using Hukuk3K.DAL.Abstract;
using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hukuk3K.UI.Controllers
{
    public class HomeController : Controller
    {
        IKullaniciDAL _kullaniciDAL;

        public HomeController(IKullaniciDAL kullaniciDAL)
        {
            _kullaniciDAL = kullaniciDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}