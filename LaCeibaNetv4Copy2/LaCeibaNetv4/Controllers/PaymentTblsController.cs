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
    public class PaymentTblsController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();

        // GET: PaymentTbls
        public ActionResult Index()
        {
            var paymentTbls = db.PaymentTbls.Include(p => p.LoansTbl);
            return View(paymentTbls.ToList());
        }

        // GET: PaymentTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl paymentTbl = db.PaymentTbls.Find(id);
            if (paymentTbl == null)
            {
                return HttpNotFound();
            }
            return View(paymentTbl);
        }

        // GET: PaymentTbls/Create
        public ActionResult Create()
        {
            ViewBag.LoanId = new SelectList(db.LoansTbls, "Id", "Id");
            return View();
        }

        // POST: PaymentTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatePmtDue,DatePmtPaid,AmtPaid,LoanId")] PaymentTbl paymentTbl)
        {
            if (ModelState.IsValid)
            {
                db.PaymentTbls.Add(paymentTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoanId = new SelectList(db.LoansTbls, "Id", "Id", paymentTbl.LoanId);
            return View(paymentTbl);
        }
        public ActionResult IndPaymentCreate(int id, System.DateTime DueDate, double AmtDue = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl newPayment = new PaymentTbl();
            newPayment.LoanId = id;
            newPayment.DatePmtDue = DueDate;
            if (DueDate == null)
            {
                DueDate = new DateTime(1981, 03, 01);
            }
            newPayment.AmtPaid = (decimal)AmtDue;
            return View(newPayment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndPaymentCreate([Bind(Exclude = "Id")] PaymentTbl paymentTbl)
        {
            if (ModelState.IsValid)
            {
                db.PaymentTbls.Add(paymentTbl);
                db.SaveChanges();
                return RedirectToAction("IndPaymentShow", new {id = paymentTbl.LoanId});
            }

            ViewBag.LoanId = new SelectList(db.LoansTbls, "Id", "Id", paymentTbl.LoanId);
            return View(paymentTbl);
        }

        // GET: PaymentTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl paymentTbl = db.PaymentTbls.Find(id);
            if (paymentTbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoanId = new SelectList(db.LoansTbls, "Id", "Id", paymentTbl.LoanId);
            return View(paymentTbl);
        }

        // POST: PaymentTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatePmtDue,DatePmtPaid,AmtPaid,LoanId")] PaymentTbl paymentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoanId = new SelectList(db.LoansTbls, "Id", "Id", paymentTbl.LoanId);
            return View(paymentTbl);
        }

        // GET: PaymentTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTbl paymentTbl = db.PaymentTbls.Find(id);
            if (paymentTbl == null)
            {
                return HttpNotFound();
            }
            return View(paymentTbl);
        }

        public ActionResult IndPaymentShow(int id)
        {
            LoansTbl loaninfo = db.LoansTbls.Find(id);
            PPlanHold holder = new PPlanHold();
            holder.CreatePlan(Convert.ToInt32(loaninfo.Instalments), (int)loaninfo.RepFreqId, (float)loaninfo.AmtLoan, loaninfo.RoundTbl.ProgramClientTbl.ProgramTbl.Program, loaninfo.TransferDate, (double)loaninfo.PaymentTbls.Sum(x => x.AmtPaid) );
            ViewBag.plan = holder.Plan;
            ViewBag.Total = holder.TotalOwed;
            return View(loaninfo);
        }

        // POST: PaymentTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentTbl paymentTbl = db.PaymentTbls.Find(id);
            db.PaymentTbls.Remove(paymentTbl);
            db.SaveChanges();
            return RedirectToAction("IndPaymentShow", new { id = paymentTbl.LoanId }); ;
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
