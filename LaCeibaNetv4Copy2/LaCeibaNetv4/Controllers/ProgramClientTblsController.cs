using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaCeibaNetv4.Models;

namespace LaCeibaNetv4.Controllers
{
    public class ProgramClientTblsController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();

        // GET: ProgramClientTbls
        public ActionResult Index()
        {
            var programClientTbls = db.ProgramClientTbls.Include(p => p.ClientsTbl).Include(p => p.ProgramTbl);
            return View(programClientTbls.ToList());
        }

        // GET: ProgramClientTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramClientTbl programClientTbl = db.ProgramClientTbls.Find(id);
            if (programClientTbl == null)
            {
                return HttpNotFound();
            }
            return View(programClientTbl);
        }

        // GET: ProgramClientTbls/Create
        public ActionResult Create()
        {
            var clients = (from m in db.ClientsTbls
                           select new SelectListItem
                           {
                               Text = m.FirstName + " " + m.MiddleName1 + " " + m.MiddleName2 + " " + m.LastName,
                               Value = m.Id.ToString()
                           });

            ViewBag.ClientsList = new SelectList(clients, "Value", "Text");
            ViewBag.ProgramId = new SelectList(db.ProgramTbls, "Id", "Program");
            return View();
        }

        // POST: ProgramClientTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,ProgramId")] ProgramClientTbl programClientTbl)
        {
            if (ModelState.IsValid)
            {
                db.ProgramClientTbls.Add(programClientTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.ClientsTbls, "Id", "FirstName", programClientTbl.ClientId);
            ViewBag.ProgramId = new SelectList(db.ProgramTbls, "Id", "Program", programClientTbl.ProgramId);
            return View(programClientTbl);
        }

        // GET: ProgramClientTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramClientTbl programClientTbl = db.ProgramClientTbls.Find(id);
            if (programClientTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.ClientsTbls, "Id", "FirstName", programClientTbl.ClientId);
            ViewBag.ProgramId = new SelectList(db.ProgramTbls, "Id", "Program", programClientTbl.ProgramId);
            return View(programClientTbl);
        }

        // POST: ProgramClientTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,ProgramId")] ProgramClientTbl programClientTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programClientTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.ClientsTbls, "Id", "FirstName", programClientTbl.ClientId);
            ViewBag.ProgramId = new SelectList(db.ProgramTbls, "Id", "Program", programClientTbl.ProgramId);
            return View(programClientTbl);
        }

        // GET: ProgramClientTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramClientTbl programClientTbl = db.ProgramClientTbls.Find(id);
            if (programClientTbl == null)
            {
                return HttpNotFound();
            }
            return View(programClientTbl);
        }

        public ActionResult IndRoundShow(int id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramClientTbl programclientsTbl = db.ProgramClientTbls.Find(id);
            if (programclientsTbl == null)
            {
                return HttpNotFound();
            }
            return View(programclientsTbl);
        }

        // POST: ProgramClientTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramClientTbl programClientTbl = db.ProgramClientTbls.Find(id);
            db.ProgramClientTbls.Remove(programClientTbl);
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
