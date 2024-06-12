using KONE.DataAccess.KONE.Abstract;
using KONE.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "CurrentCardAddressLandList")]
    public class CurrentCardAddressLandList : ViewComponent
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CurrentCardAddressLandList(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(int id, int landnameid)
        {
            ViewBag.CurrentCardId = id;
            ViewBag.LandNameId = landnameid;
            var landNames = await _unitOfWork.CurrentCardAddressMapping.GetAllAsync(c => c.CurrentCardId == id && c.CurrentCardLandNameId == landnameid, c => c.Address, c => c.Address.Province, c => c.Address.District, c => c.Address.Village, c => c.CurrentCardLandName);
            if (landNames != null)
                return View(landNames.ToList());
            else
                return View(new List<CurrentCardAddressMapping>());
        }
        #endregion
    }
}
