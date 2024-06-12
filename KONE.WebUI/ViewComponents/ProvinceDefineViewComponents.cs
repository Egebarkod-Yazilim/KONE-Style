using Microsoft.AspNetCore.Mvc;

namespace KONE.KOne.WebUI.ViewComponents
{
    [ViewComponent(Name = "ProvinceDefineViewComponents")]

    public class ProvinceDefineViewComponents : ViewComponent
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
