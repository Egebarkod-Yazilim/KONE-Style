using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "ProductTypeDefineViewComponents")]

    public class ProductTypeDefineViewComponents : ViewComponent
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
