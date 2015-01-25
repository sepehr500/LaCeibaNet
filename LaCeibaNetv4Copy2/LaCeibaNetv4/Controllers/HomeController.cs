using LaCeibaNetv4.Models;
using LaCeibaNetv4.ToolsStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaCeibaNetv4.Controllers
{
    public class HomeController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();
        public ActionResult Index()
        {
            ViewBag.ActiveClients = db.activeClients();
            ViewBag.RepaymentRate = db.repaymentRate().ToString("#.##");
            ViewBag.GLP = db.grossLoanPortfolio().ToString("#.##");
            ViewBag.ALS = db.averageLoanSize().ToString("#.##");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}