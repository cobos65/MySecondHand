using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySecondHand.Spider.Interfaces;
using MySecondHand.Spider.Model;

namespace MySecondHand.Web.Controllers
{
    public class BrowserController : Controller
    {
        ISpiderManager _spiderManager;

        public BrowserController(ISpiderManager spiderManager)
        {
            _spiderManager = spiderManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchProducts(string searchKey)
        {
            var searchParameter = new SearchParameter()
            {
                SearchKey = searchKey,
                Latitude = null,
                Longitude = null
            };
            var products = _spiderManager.SearchItems(searchParameter);

            return View(products);
        }
    }
}