using Microsoft.AspNetCore.Mvc;

namespace Konfrut.KOne.WebUI.ViewComponents
{

    [ViewComponent(Name = "DistrictDefineViewComponents")]

    public class DistrictDefineViewComponents : ViewComponent
    {
        #region Fields
        #endregion

        #region Ctor
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
        #endregion
    }
}
