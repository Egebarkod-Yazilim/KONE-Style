
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KONE.WebUI.Models.CBSAPI
{
    public class CbsApiViewModel
    {
        public CbsApiViewModel()
        {
            Provinces = new List<SelectListItem>();
            Districts = new List<SelectListItem>();
        }

        public int SelectedProvinceId { get; set; }
        public int SelectedDistrictId { get; set; }
        public List<SelectListItem> Provinces { get; set; }
        public List<SelectListItem> Districts { get; set; }
    }
}
