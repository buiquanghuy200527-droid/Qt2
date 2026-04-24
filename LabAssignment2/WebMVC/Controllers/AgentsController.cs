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
    public class AgentsController : Controller
    {
        private Lab2DBEntities2 db = new Lab2DBEntities2();

        private bool IsLoggedIn()
        {
            return Session["UserName"] != null;
        }

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View(db.Agents.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        public ActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgentID,AgentName,Address")] Agent agent)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                db.Agents.Add(agent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        public ActionResult Edit(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgentID,AgentName,Address")] Agent agent)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        public ActionResult Delete(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            Agent agent = db.Agents.Find(id);
            db.Agents.Remove(agent);
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
