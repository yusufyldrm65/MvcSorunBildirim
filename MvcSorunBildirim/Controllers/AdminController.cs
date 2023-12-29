using MvcSorunBildirim.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MvcSorunBildirim.Models;

namespace YazilimVan.Controllers
{
    public class AdminController : Controller
    {
        private Sorun_BildirimEntities db = new Sorun_BildirimEntities();

        public ActionResult Index()
        {
            var basvuru = db.Basvuru.Include(b => b.Kurum).Include(b => b.Yetkililer);
            return View(basvuru.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basvuru basvuru = db.Basvuru.Find(id);
            if (basvuru == null)
            {
                return HttpNotFound();
            }
            return View(basvuru);
        }





        public ActionResult CevapEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cevaplar cevaplar = db.Cevaplar.Find(id);
            if (cevaplar == null)
            {
                return HttpNotFound();
            }

            if (cevaplar.Basvuru_Id == null)
            {
                ViewData["Hata"] = "Başvuru ID gereklidir.";

                return View(cevaplar);
            }

            TempData["basvuru_id"] = cevaplar.Basvuru_Id;

            string cevapMetni = Request.Form["cevap_metni"];

            cevaplar.Cevap_Metni = cevapMetni;

            cevaplar.Basvuru_Id = (int)TempData["basvuru_id"];

            db.SaveChanges();

            return RedirectToAction("Cevaplar");
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CevapEdit([Bind(Include = "cevap_id,cevap_zamani,cevap_metni,basvuru_id,is_active")] Cevaplar cevaplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cevaplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.basvuru_id = new SelectList(db.Basvuru, "basvuru_id", "konum", cevaplar.Basvuru_Id);
            return View(cevaplar);
        }




        // GET: DurumEdit
        public ActionResult DurumEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basvuru basvuru = db.Basvuru.Find(id);
            if (basvuru == null)
            {
                return HttpNotFound();
            }

            ViewBag.kurum_id = new SelectList(db.Kurum, "kurum_id", "kurum_adi", basvuru.Kurum_Id);
            ViewBag.yetkili_id = new SelectList(db.Yetkililer, "yetkili_id", "ad", basvuru.Yetkili_Id);
            return View(basvuru);
        }




        // POST: DurumEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DurumEdit([Bind(Include = "basvuru_id,konum,basvuru_zamani,basvuru_durumu,dogrulanan_kisi,sorun_turu_id,kurum_id,yetkili_id,is_active")] Basvuru basvuru)
        {
            if (ModelState.IsValid)
            {
                var existingBasvuru = db.Basvuru.Find(basvuru.Basvuru_Id);
                if (existingBasvuru != null)
                {
                    UpdateModel(existingBasvuru, new string[] { "konum", "basvuru_zamani", "basvuru_durumu", "dogrulanan_kisi", "sorun_turu_id", "kurum_id", "yetkili_id", "is_active" });

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.kurum_id = new SelectList(db.Kurum, "kurum_id", "kurum_adi", basvuru.Kurum_Id);
            ViewBag.yetkili_id = new SelectList(db.Yetkililer, "yetkili_id", "ad", basvuru.Yetkili_Id);
            return View(basvuru);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basvuru basvuru = db.Basvuru.Find(id);
            if (basvuru == null)
            {
                return HttpNotFound();
            }
            return View(basvuru);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Basvuru basvuru = db.Basvuru.Find(id);

            var cevaplar = db.Cevaplar.Where(c => c.Basvuru_Id == basvuru.Basvuru_Id);

            foreach (var cevap in cevaplar)
            {
                db.Cevaplar.Remove(cevap);
            }

            db.Basvuru.Remove(basvuru);

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}