using KONE.Business.CBSAPI;
using KONE.WebUI.Models.CBSAPI;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "CbsApiViewComponent")]

    public class CbsApiViewComponent : ViewComponent
    {
        #region Fields
        private readonly ICbsApiService _cbsApiService;
        #endregion

        #region Ctor
        public CbsApiViewComponent(ICbsApiService cbsApiService)
        {
            _cbsApiService = cbsApiService;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newCbsApiModel = new CbsApiViewModel();

            var provinces = await _cbsApiService.GetProvinceList();

            if (provinces.features != null)
            {
                foreach (var province in provinces.features)
                {
                    newCbsApiModel.Provinces.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(province.properties.text, province.properties.id.ToString()));
                }
            }


            return View(newCbsApiModel);
        }
        #endregion
    }
}
