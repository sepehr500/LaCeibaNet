using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaCeibaNetv4.Models;
using LaCeibaNetv4.Models.Classes;

namespace LaCeibaNetv4.Controllers
{
    public class LoansTblsController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();

        // GET: LoansTbls
        public ActionResult Index(string searchString)
        {
            var loansTbls = db.LoansTbls.Include(l => l.RoundTbl);
            if (!String.IsNullOrEmpty(searchString))
            {
                loansTbls = loansTbls.Where(s => s.RoundTbl.ProgramClientTbl.ClientsTbl.FirstName.ToUpper().Contains(searchString.ToUpper())
                                       || s.RoundTbl.ProgramClientTbl.ClientsTbl.MiddleName1.ToUpper().Contains(searchString.ToUpper()) || s.RoundTbl.ProgramClientTbl.ClientsTbl.MiddleName2.ToUpper().Contains(searchString.ToUpper()) || s.RoundTbl.ProgramClientTbl.ClientsTbl.LastName.ToUpper().Contains(searchString.ToUpper()) || s.RoundTbl.ProgramClientTbl.ProgramTbl.Program.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(loansTbls.ToList());
        }
        
        public ActionResult PayPlanCreate([Bind(Include = "Id,AmtLoan,TransferDate,Active,RoundId")] LoansTbl loansTbl, string ProgramName)
        {
            return View();
        }
        // GET: LoansTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoansTbl loansTbl = db.LoansTbls.Find(id);
            if (loansTbl == null)
            {
                return HttpNotFound();
            }
            return View(loansTbl);
        }


        // GET: LoansTbls/Create
        public ActionResult Create()
        {
            ViewBag.RoundId = new SelectList(db.RoundTbls, "Id", "Id");
            return View();
        }

        // POST: LoansTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AmtLoan,TransferDate,Active,RoundId")] LoansTbl loansTbl)
        {
            if (ModelState.IsValid)
            {
                db.LoansTbls.Add(loansTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoundId = new SelectList(db.RoundTbls, "Id", "Id", loansTbl.RoundId);
            return View(loansTbl);
        }

        // GET: LoansTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoansTbl loansTbl = db.LoansTbls.Find(id);
            if (loansTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoundId = new SelectList(db.RoundTbls, "Id", "Id", loansTbl.RoundId);
            return View(loansTbl);
        }

        // POST: LoansTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AmtLoan,TransferDate,Active,RoundId,RepFreqId,Instalments")] LoansTbl loansTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loansTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoundId = new SelectList(db.RoundTbls, "Id", "Id", loansTbl.RoundId);
            return View(loansTbl);
        }

        public ActionResult IndLoanCreate(int? id)
        {
            
            
            ViewBag.RoundId = (int)id;
            RoundTbl RoundTbl = db.RoundTbls.Find(id);
            ViewBag.ProgramName = RoundTbl.ProgramClientTbl.ProgramTbl.Program;
            ViewBag.sugAmt = RoundTbl.LoanAmt();
            LoansTbl newloan = new LoansTbl();
            newloan.AmtLoan = RoundTbl.LoanAmt();
            ViewBag.RepFreqId = new SelectList(db.RepFreq, "Id", "Frequency");
            return View(newloan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndLoanCreate([Bind(Exclude = "Id")] LoansTbl newloan, string Command, string Program)
        {
            if (Command == "Calculate")
            {
                PPlanHold holder = new PPlanHold();
                holder.CreatePlan(Convert.ToInt32(newloan.Instalments), newloan.RepFreqId, (float)newloan.AmtLoan, Program, newloan.TransferDate);
                ViewBag.RoundId = (int)newloan.RoundId;
                RoundTbl RoundTbl = db.RoundTbls.Find(newloan.RoundId);
                ViewBag.ProgramName = RoundTbl.ProgramClientTbl.ProgramTbl.Program;
                ViewBag.sugAmt = RoundTbl.LoanAmt();
                LoansTbl newloan2 = new LoansTbl();
                newloan.AmtLoan = RoundTbl.LoanAmt();
                ViewBag.RepFreqId = new SelectList(db.RepFreq, "Id", "Frequency");
                ViewBag.plan = holder.Plan;
                return View(newloan2);

            }
            else
            {


                if (ModelState.IsValid)
                {
                    db.LoansTbls.Add(newloan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RoundId = new SelectList(db.RoundTbls, "Id", "Id", newloan.RoundId);

                return View(newloan);
            }
        }

        // GET: LoansTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoansTbl loansTbl = db.LoansTbls.Find(id);
            if (loansTbl == null)
            {
                return HttpNotFound();
            }
            return View(loansTbl);
        }

        // POST: LoansTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoansTbl loansTbl = db.LoansTbls.Find(id);
            db.LoansTbls.Remove(loansTbl);
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
