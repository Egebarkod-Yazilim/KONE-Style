using KONE.DataAccess.KONE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.Controllers
{
    public class StatisticsVisualizationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatisticsVisualizationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Graphs()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProvinceList()
        {
            var provinces = await _unitOfWork.Province.GetAllAsync();
            return Json(provinces);
        }
    }
}
