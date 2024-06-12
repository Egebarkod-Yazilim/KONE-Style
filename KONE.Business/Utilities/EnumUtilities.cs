
using KONE.Shared.Utils.Results.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KONE.Business.Utilities
{
    public static class EnumUtilities
    {

        public static List<SelectListItem> ConvertEnumToSelectListItem(List<EnumListItem> enums, string selectedItem = null)
        {
            var list = new List<SelectListItem>();

            foreach (var item in enums)
            {
                if (item.Name == selectedItem)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }


            }

            return list;
        }
    }
}
