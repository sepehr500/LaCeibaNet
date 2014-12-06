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
    public class ClientsTblsController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();

        // GET: ClientsTbls
        public ActionResult Index(string sortOrder, string searchString)
        {
            //var clientsTbls = db.ClientsTbls.Include(c => c.CenterTbl);
            var clientTbls = from s in db.ClientsTbls
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clientTbls= clientTbls.Where(s => s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                       || s.MiddleName1.ToUpper().Contains(searchString.ToUpper()) || s.MiddleName2.ToUpper().Contains(searchString.ToUpper()) || s.LastName.ToUpper().Contains(searchString.ToUpper()) );
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                case "name_desc":
                    clientTbls = clientTbls.OrderByDescending(s => s.LastName);
                    break;
                //case "Date":
                //    clientTbls = clientTbls.OrderBy(s => s.EnrollmentDate);
                //    break;
                //case "date_desc":
                //    students = students.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    clientTbls = clientTbls.OrderBy(s => s.LastName);
                    break;
            }
            return View(clientTbls.ToList());



        }

        // GET: ClientsTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientsTbl clientsTbl = db.ClientsTbls.Find(id);
            if (clientsTbl == null)
            {
                return HttpNotFound();
            }
            var test = (from m in db.ProgramClientTbls
                        where m.ClientId == clientsTbl.Id
                        select new SelectListItem
                        {
                            Text = m.ProgramTbl.Program,
                            Value = m.Id.ToString() 
                        });

            ViewBag.testit = new SelectList(test, "Value", "Text");
            return View(clientsTbl);
        }

        // GET: ClientsTbls/Create
        public ActionResult Create()
        {
            ViewBag.CenterId = new SelectList(db.CenterTbls, "Id", "Center");
            return View();
        }

        // POST: ClientsTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,CenterId,MiddleName1,MiddleName2,DateAdded,BirthDay")] ClientsTbl clientsTbl)
        {
            if (ModelState.IsValid)
            {

                clientsTbl.DateAdded = DateTime.Now;
                clientsTbl.Active = true;
                db.ClientsTbls.Add(clientsTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CenterId = new SelectList(db.CenterTbls, "Id", "Center", clientsTbl.CenterId);
            return View(clientsTbl);
        }

        // GET: ClientsTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientsTbl clientsTbl = db.ClientsTbls.Find(id);
            if (clientsTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.CenterId = new SelectList(db.CenterTbls, "Id", "Center", clientsTbl.CenterId);
            return View(clientsTbl);
        }

        // POST: ClientsTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,CenterId,MiddleName1,MiddleName2,DateAdded, Active")] ClientsTbl clientsTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientsTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = new SelectList(db.CenterTbls, "Id", "Center", clientsTbl.CenterId);
            return View(clientsTbl);
        }

        // GET: ClientsTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientsTbl clientsTbl = db.ClientsTbls.Find(id);
            if (clientsTbl == null)
            {
                return HttpNotFound();
            }
            return View(clientsTbl);
        }

        // POST: ClientsTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientsTbl clientsTbl = db.ClientsTbls.Find(id);
            db.ClientsTbls.Remove(clientsTbl);
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
