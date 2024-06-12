using KONE.DataAccess.KONE.Abstract;
using KONE.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "CurrentCardLandNameListViewComponent")]

    public class CurrentCardLandNameListViewComponent : ViewComponent
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CurrentCardLandNameListViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.CurrentCardId = id;
            var landNames = await _unitOfWork.CurrentCardLandName.GetAllAsync(c => c.CurrentCardId == id);
            if (landNames != null)
                return View(landNames.ToList());
            else
                return View(new List<CurrentCardLandName>());
        }
        #endregion
    }
}
