using KONE.DataAccess.KONE.Abstract;
using KONE.Entities.Dtos.CurrentCard;
using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "CurrentCardAddressesViewComponent")]

    public class CurrentCardAddressesViewComponent : ViewComponent
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CurrentCardAddressesViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(int currentCardId)
        {
            var currentCardAddressList = new CurrentCardAddressList();
            currentCardAddressList.CurrentCardId = currentCardId;
            var addressMappings = await _unitOfWork.CurrentCardAddressMapping.GetAllAsync(c => c.CurrentCard.Id == currentCardId, c => c.CurrentCard, c => c.Address, c => c.Address.Country, c => c.Address.Province, c => c.Address.District, c => c.Address.Village);

            foreach (var item in addressMappings)
            {
                if (item.Address != null)
                    currentCardAddressList.Addresses.Add(item.Address);
            }

            return View(currentCardAddressList);
        }
        #endregion
    }
}
