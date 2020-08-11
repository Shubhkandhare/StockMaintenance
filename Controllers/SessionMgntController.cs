using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockMaintenance.Controllers
{
    public class SessionMgntController : Controller
    {
        // GET: SessionMgnt
        public ActionResult Index()
        {
            TempData["Name"] = "Shubh";
            TempData["Age"] = 32;

            return RedirectToAction("AboutUs");
        }

        public ActionResult AboutUs()
        {
            string strName = TempData["Name"].ToString();
            string strAge = TempData["Age"].ToString();

            ViewData["PassName"] = strName;
            ViewData["PassAge"] = strAge;
            return View();
        }
    }
}