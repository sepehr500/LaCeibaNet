using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using LaCeibaNetv4.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaCeibaNetv4.ToolsStuff;

namespace LaCeibaNetv4.Controllers
{
    public class AnalysisController : Controller
    {
        private LaCeibaDbv4Entities db = new LaCeibaDbv4Entities();
        // GET: Analysis
        public ActionResult Index()
        {
            Highcharts chart = ToolsStuff.Charts.StatusPerLevel(db); 
                

            return View(chart);
        }


        public ActionResult Charts()
        {
            return View();
        }
    }
}