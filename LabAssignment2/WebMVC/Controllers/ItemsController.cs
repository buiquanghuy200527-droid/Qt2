using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC;

namespace WebMVC.Controllers
{
    public class ItemsController : Controller
    {
        private Lab2DBEntities2 db = new Lab2DBEntities2();

        private bool IsLoggedIn()
        {
            return Session["UserName"] != null;
        }

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View(db.Items.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemName,Size,Price")] Item item)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemName,Size,Price")] Item item)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Delete(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
