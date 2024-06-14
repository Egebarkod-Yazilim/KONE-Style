using Microsoft.AspNetCore.Mvc;

namespace KONE.KOne.WebUI.Controllers
{
    public class DefineController : Controller
    {
        public DefineController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Districts()
        {
            return ViewComponent("DistrictDefineViewComponents");

        }

        public IActionResult Provinces()
        {
            return ViewComponent("ProvinceDefineViewComponents");
        }

        public IActionResult Villages()
        {
            return ViewComponent("VillagesDefineViewComponents");
        }

        public IActionResult Neighbourhood()
        {
            return ViewComponent("NeighbourhoodDefineViewComponents");
        }

        public IActionResult Countries()
        {
            return ViewComponent("CountriesDefineViewComponents");
        }

        public IActionResult Plates()
        {
            return ViewComponent("PlatesDefineViewComponents");
        }

        public IActionResult Facilities()
        {
            return ViewComponent("FacilitiesDefineViewComponents");
        }

        public IActionResult ColorTypes()
        {
            return ViewComponent("ColorsDefineViewComponents");
        }
        public IActionResult QualityManagementQuestions()
        {
            return ViewComponent("QualityManagementQuestionsViewComponents");
        }

        public IActionResult TasteCodes()
        {
            return ViewComponent("TasteCodesViewComponents");
        }

        public IActionResult UnitCodes()
        {
            return ViewComponent("UnitCodesViewComponents");
        }

        public IActionResult SettingsDefine()
        {
            return ViewComponent("SettingsDefineViewComponents");
        }

        public IActionResult ProductTypes()
        {
            return ViewComponent("ProductTypeDefineViewComponents");
        }
    }
}
