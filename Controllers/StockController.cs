using Models;
using Repositories.RepositoryImplementation;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockMaintenance.Controllers
{
    public class StockController : Controller
    {
        DataAccess dataAccess = new DataAccess(ConfigurationManager.ConnectionStrings["StockManagement"].ToString());
        StockService _stockService;

        public StockController()
        {
            _stockService = new StockService(new StockRepository(dataAccess));
        }
        // GET: Stock
        public ActionResult Index(LoginModel loginModel)
        {
            ViewBag.UserName = loginModel.UserName;
            DataTable dtStock = new DataTable();
            dtStock = _stockService.GetAllStock();
            List<StockModel> stockModels = new List<StockModel>();
            stockModels = (from DataRow dr in dtStock.Rows
                           select new StockModel()
                           {
                               Id = Convert.ToInt32(dr["Id"]),
                               ItemId = dr["ItemId"].ToString(),
                               ItemName = dr["ItemName"].ToString(),
                               Unit = Convert.ToInt32(dr["Unit"].ToString()),
                               Price = Convert.ToDecimal(dr["Price"])
                           }).ToList();
            return View(stockModels);
        }
        [HttpPost]
        public ActionResult Create(StockModel stockModel)
        {
            bool bResult = _stockService.CreateStock(stockModel);
            return View(stockModel);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(StockModel stockModel)
        {
            bool bResult = _stockService.UpdateStock(stockModel);
            return View(stockModel);
        }
        public ActionResult Edit(string Id)
        {
            if (ModelState.IsValid)
            {
                StockModel stockModels = new StockModel();
                stockModels = _stockService.GetStockById(Id);
                return View(stockModels);
            }
            else
                return View();
        }
        public ActionResult Delete(string Id)
        {
            bool bResult = _stockService.DeleteStock(Id);
            return View();
        }
        public ActionResult Details(string Id)
        {
            StockModel stockModels = new StockModel();
            stockModels = _stockService.GetStockById(Id);
            return View(stockModels);
        }
    }
}