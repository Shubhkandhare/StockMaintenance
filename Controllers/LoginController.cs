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
            return View();
        }
    }
}