using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MvcSorunBildirim.Models;

namespace MvcSorunBildirim.Controllers
{
    public class LoginController : Controller
    {
        Sorun_BildirimEntities db = new Sorun_BildirimEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Yetkililer p)
        {
            var verigetir = db.Yetkililer.FirstOrDefault(x => x.Email == p.Email && x.Sifre == p.Sifre);
            if(verigetir!=null)
            {
                FormsAuthentication.SetAuthCookie(verigetir.Email, false);

                return RedirectToAction("Index", "Basvuru");
            }
            else
            {
                ViewBag.hata = "Kullanıcı mail veya şifre hatalı";
            }
            return View();
        }
    }
}