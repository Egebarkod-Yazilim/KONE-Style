using KONE.Business.CBSAPI;
using Konfrut.Business.Services.Abstract;
using Konfrut.KOne.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Konfrut.KOne.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICbsApiService _bsApiService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICbsApiService bsApiService)
        {
            _logger = logger;
            _productService = productService;
            _bsApiService = bsApiService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetPagedListAsync(1, 25);
            return View(products);
        }
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetPagedListAsync(1, int.MaxValue);
            var countOfProducts = await _productService.GetCountAsync();
            var productList = products.ToList();
            return Json(new { data = productList, draw = 1, recordsTotal = countOfProducts, recordsFiltered = products.Count });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task<IActionResult> Defines()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ProvinceList(int provinceId)
        {
            var provinces = await _bsApiService.GetProvinceList();

            var province = provinces.features.FirstOrDefault(c => c.properties.id == provinceId);

            return Json(province);
        }

        [HttpGet]
        public async Task<IActionResult> DistrictList(int provinceId)
        {
            var provinces = await _bsApiService.GetDistrictList(provinceId);

            return Json(provinces);
        }

    }
}