using KONE.Business.CBSAPI;
using KONE.WebUI.Models.CBSAPI;
using KONE.DataAccess.KONE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "CbsApiViewComponent")]

    public class CbsApiViewComponent : ViewComponent
    {
        #region Fields
        private readonly ICbsApiService _cbsApiService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CbsApiViewComponent(ICbsApiService cbsApiService, IUnitOfWork unitOfWork)
        {
            _cbsApiService = cbsApiService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newCbsApiModel = new CbsApiViewModel();

            var provinces = await _unitOfWork.Province.GetAllAsync();

            if (provinces.Count > 0)
            {

                foreach (var province in provinces)
                {
                    newCbsApiModel.Provinces.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(province.Name, province.PropertyId.ToString()));
                }
            }
            else
            {
                var provincesFromApi = await _cbsApiService.GetProvinceList();

                if (provincesFromApi.features != null)
                {
                    foreach (var province in provincesFromApi.features)
                    {
                        newCbsApiModel.Provinces.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(province.properties.text, province.properties.id.ToString()));
                    }
                }
            }




            return View(newCbsApiModel);
        }
        #endregion
    }
}
