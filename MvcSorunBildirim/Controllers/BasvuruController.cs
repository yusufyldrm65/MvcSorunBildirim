using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSorunBildirim.Globals;
using MvcSorunBildirim.Models;

namespace MvcSorunBildirim.Controllers
{
    public class BasvuruController : Controller
    {
        // GET: Basvuru
        Sorun_BildirimEntities db = new Sorun_BildirimEntities();
        public ActionResult Index()
        {
            var Kurumlar = db.Kurum.OrderBy(i=>i.Kurum_Adi).ToList();
            ViewBag.Kurumlar = new SelectList(Kurumlar, "Kurum_Id", "Kurum_Adi");
            return View();
        }



        [HttpPost]
        public ActionResult Index(Basvuru basvuru)
        {
            db.Basvuru.Add(basvuru);
            db.SaveChanges();
            if (Request.Files.Count > 0)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        Dokumanlar dokuman = new Dokumanlar();
                        dokuman.Basvuru_Id = basvuru.Basvuru_Id;


                        var fileName = Path.GetFileName(file.FileName);
                        var filetype = file.FileName.Substring(file.FileName.LastIndexOf(".")+1);
                        dokuman.Dosya_Tipi = filetype;
                        dokuman.Dosya_Boyutu = file.ContentLength;
                        var dosyaadi = "Dosya_" + basvuru.Basvuru_Id.ToString() + "_" + DateTime.Now.Ticks.ToString() + "." + filetype;
                        dokuman.Dosya_Adi = dosyaadi;

                        var path = Path.Combine(Server.MapPath("~/Files/Uploads/"), dosyaadi);
                        file.SaveAs(path);

                        db.Dokumanlar.Add(dokuman);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Basvuru2", new { id = basvuru.Basvuru_Id });
        }
        public ActionResult Basvuru2(int id)
        {
            var basvuru = db.Basvuru.Find(id);
            return View(basvuru);
        }


        [HttpPost]
        public ActionResult Basvuru2(Basvuru basvuru)
        {
            var obasvuru = db.Basvuru.Find(basvuru.Basvuru_Id);
            if (obasvuru != null)
            {
                obasvuru.AdSoyad = basvuru.AdSoyad;
                obasvuru.Email = basvuru.Email;
                obasvuru.Telefon = basvuru.Telefon;

                BasvuruMail email = new BasvuruMail();
                List<string> to = new List<string>();
                to.Add(basvuru.Email);
                
                Random rnd = new Random();
                var rastgelesayi = rnd.Next(100000,999999);

                string emailMessage = "<b>Doğrulama Kodunuz:</b>" + rastgelesayi.ToString(); ;
                email.Send(to, null, null, "Bildirim Doğrulama Kodu", emailMessage);
                obasvuru.Dogrulama_Kodu = rastgelesayi.ToString();
                db.Entry(obasvuru).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Basvuru3", new { id = basvuru.Basvuru_Id });
        }
        public ActionResult Basvuru3(int id)
        {
            var basvuru = db.Basvuru.Find(id);
            return View(basvuru);
        }

        [HttpPost]
        public ActionResult Basvuru3(Basvuru basvuru)
        {
            var obasvuru = db.Basvuru.Find(basvuru.Basvuru_Id);

            if(obasvuru.Dogrulama_Kodu == basvuru.Dogrulama_Kodu)
            {
                obasvuru.Is_Active = true;
                obasvuru.Basvuru_Durumu = "Başvuru Yapıldı";
                db.Entry(obasvuru).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ViewBag.HataMesaj = "Doğrulama kodu yanlış!";
                return RedirectToAction("Basvuru2", new { id = obasvuru.Basvuru_Id });
            }

            return RedirectToAction("BasvuruTamamlandi");
        }

        public ActionResult BasvuruTamamlandi()
        {
            return View();
        }
        public ActionResult DosyaYukle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DosyaYukle(System.Web.HttpPostedFileBase yuklenecekDosya)
        {
            if (yuklenecekDosya != null)
            {
                string dosyaYolu = Path.GetFileName(yuklenecekDosya.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Dosyalar"), dosyaYolu);
                yuklenecekDosya.SaveAs(yuklemeYeri);
            }
            return View();
        }
    }

}

