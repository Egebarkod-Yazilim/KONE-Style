using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{

    [ViewComponent(Name = "VillagesDefineViewComponents")]

    public class VillagesDefineViewComponents : ViewComponent
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
