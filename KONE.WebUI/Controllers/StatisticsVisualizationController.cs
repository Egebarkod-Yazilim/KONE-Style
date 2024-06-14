using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.Controllers
{
    public class StatisticsVisualizationController : Controller
    {
        public StatisticsVisualizationController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Graphs()
        {
            return View();
        }
    }
}
