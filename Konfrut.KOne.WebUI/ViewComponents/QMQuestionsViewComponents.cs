using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{

    [ViewComponent(Name = "QMQuestionsViewComponents")]

    public class QMQuestionsViewComponents : ViewComponent
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
