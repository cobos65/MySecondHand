using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySecondHand.Spider.Interfaces;
using MySecondHand.Spider.Model;
using MySecondHand.Web2.ViewModels;

namespace MySecondHand.Web2.Controllers
{
    public class HomeController : Controller
    {
        ISpiderManager _spiderManager;

        public HomeController(ISpiderManager spiderManager)
        {
            _spiderManager = spiderManager;
        }

        public IActionResult Index()
        {
            var products = new List<ProductItemViewModel>();
            return View(products);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
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
            List<ProductItemViewModel> productItemViewModels = new List<ProductItemViewModel>();
            foreach (var productItem in products)
            {
                var productItemViewModel = new ProductItemViewModel()
                {
                    ItemName= productItem.ItemName,
                    ItemPrice = productItem.ItemPrice,
                    ItemSource= productItem.ItemSource,
                    ItemImage = productItem.ItemImage
                };
                productItemViewModels.Add(productItemViewModel);
            }

            return View("Index", productItemViewModels);
        }
    }
}
