using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories.RepositoryImplementation;
using Services;
using Models;

namespace StockMaintenance.Controllers
{
    public class LoginController : Controller
    {
        DataAccess dataAccess = new DataAccess(ConfigurationManager.ConnectionStrings["StockManagement"].ToString());
        LoginService _loginService;

        public LoginController()
        {
            _loginService = new LoginService(new LoginRepository(dataAccess));
        }
        // GET: Login
        public ActionResult Index()
        {
            string strUserName = "shubh.kandhare@gmail.com";
            LoginModel loginModel = new LoginModel();
            loginModel = _loginService.getAccountDetails(strUserName);
            return View(loginModel);
        }
        public ActionResult Login()
        {
            ///It will passing Layout name to View so as Login View can use _login layout for master template
            return View("Login", "_login");
        }

        public ActionResult Index1() 
        {
            return View();
        }
        public ActionResult Sum(int a, int b)
        {
            int c = a + b;
            if (a == 0 || b == 0) { throw new Exception("Input parameters are null."); }
            ViewBag.Sum = c;
            TempData["Result"] = c;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            LoginModel retrieveLoginInfo = _loginService.getAccountDetails(loginModel.UserName);
            if (retrieveLoginInfo.IsValid)
            {
                ViewBag.ResultMessage = "It is valid user. Welcome " + retrieveLoginInfo.UserName;
                return RedirectToAction("Index", "Stock", loginModel);
                ///* ViewBag is one of the state management techninque in MVC
                ///* It is use to transfer data from Controller to View
                ///* ViewBag is an object of Dynamic propery which was introduced in C#4.0
                ///* It is wrapper around ViewData 
                ///* typecasting is not required                
            }
            else
                ViewBag.ResultMessage = "It is fake user";
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}