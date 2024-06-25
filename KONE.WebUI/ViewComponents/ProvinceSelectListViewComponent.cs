using KONE.DataAccess.KONE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "ProvinceSelectListViewComponent")]
    public class ProvinceSelectListViewComponent : ViewComponent
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion


        #region Ctor
        public ProvinceSelectListViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var districts = await _unitOfWork.District.GetAllAsync();
            districts = districts.Where(c => c.Id == id).ToList();
            return View(districts);
        }
        #endregion
    }
}
