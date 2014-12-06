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
    public class RoundTblController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();

        // GET: RoundTbl
        public ActionResult Index(string searchString)
        {
            var roundTbls = db.RoundTbls.Include(r => r.ProgramClientTbl);
            if (!String.IsNullOrEmpty(searchString))
            {
                roundTbls = roundTbls.Where(s => s.ProgramClientTbl.ClientsTbl.FirstName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ProgramClientTbl.ClientsTbl.MiddleName1.ToUpper().Contains(searchString.ToUpper()) || s.ProgramClientTbl.ClientsTbl.MiddleName2.ToUpper().Contains(searchString.ToUpper()) || s.ProgramClientTbl.ClientsTbl.LastName.ToUpper().Contains(searchString.ToUpper()) || s.ProgramClientTbl.ProgramTbl.Program.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(roundTbls.ToList());
        }

        // GET: RoundTbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundTbl roundTbl = db.RoundTbls.Find(id);
            if (roundTbl == null)
            {
                return HttpNotFound();
            }
            return View(roundTbl);
        }

        // GET: RoundTbl/Create
        public ActionResult Create()
        {
            ViewBag.PClientNameId = new SelectList(db.ProgramClientTbls, "Id", "Id");
            return View();
        }

        // POST: RoundTbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PClientNameId,RoundNum")] RoundTbl roundTbl)
        {
            if (ModelState.IsValid)
            {
                db.RoundTbls.Add(roundTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PClientNameId = new SelectList(db.ProgramClientTbls, "Id", "Id", roundTbl.PClientNameId);
            return View(roundTbl);
        }

        // GET: RoundTbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundTbl roundTbl = db.RoundTbls.Find(id);
            if (roundTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.PClientNameId = new SelectList(db.ProgramClientTbls, "Id", "Id", roundTbl.PClientNameId);
            return View(roundTbl);
        }

        // POST: RoundTbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PClientNameId,RoundNum")] RoundTbl roundTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roundTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PClientNameId = new SelectList(db.ProgramClientTbls, "Id", "Id", roundTbl.PClientNameId);
            return View(roundTbl);
        }

        // GET: RoundTbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundTbl roundTbl = db.RoundTbls.Find(id);
            if (roundTbl == null)
            {
                return HttpNotFound();
            }
            return View(roundTbl);
        }

        // POST: RoundTbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoundTbl roundTbl = db.RoundTbls.Find(id);
            db.RoundTbls.Remove(roundTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AGRound(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramClientTbl AddRound = db.ProgramClientTbls.Find(id);
            int NumofRound = AddRound.RoundTbls.Count;
            NumofRound++;
            ViewBag.NumofRound = NumofRound;
            ViewBag.clientInfo = AddRound;
            RoundTbl newRndTbl = new RoundTbl();
            newRndTbl.PClientNameId = (int)id;
            newRndTbl.RoundNum = NumofRound;
            if (AddRound == null)
            {
                return HttpNotFound();
            }
            return View(newRndTbl);
        }
        public ActionResult IndLoanShow(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundTbl roundTbl = db.RoundTbls.Find(id);
            if (roundTbl == null)
            {
                return HttpNotFound();
            }
            return View(roundTbl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AGRound([Bind(Exclude = "Id")] RoundTbl roundTbl)
        {

            db.RoundTbls.Add(roundTbl);
            db.SaveChanges();
            
            return RedirectToAction( "IndRoundShow", new { controller =  "ProgramClientTbls", id = roundTbl.PClientNameId });
            
            
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
