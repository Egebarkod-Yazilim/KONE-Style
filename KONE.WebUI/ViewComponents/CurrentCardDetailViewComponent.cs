using KONE.Business.Utilities;
using KONE.DataAccess.KONE.Abstract;
using KONE.Entities.Dtos.CurrentCard;
using KONE.Shared.Utils.Results.Concrete;
using Microsoft.AspNetCore.Mvc;
using static KONE.Shared.Utils.Results.ComplexTypes.EnumVariables;

namespace KONE.WebUI.ViewComponents
{
    [ViewComponent(Name = "CurrentCardDetailViewComponent")]

    public class CurrentCardDetailViewComponent : ViewComponent
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CurrentCardDetailViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var currentCardModel = new AddOrUpdateCurrentCard();

            var currentCardTypeEnumList = ((CurrentCardTypeEnum[])Enum.GetValues(typeof(CurrentCardTypeEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
            var CompanyTypeEnumList = ((CompanyTypeEnum[])Enum.GetValues(typeof(CompanyTypeEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
            var currentCardStatuesEnumList = ((CurrentCardStatusEnum[])Enum.GetValues(typeof(CurrentCardStatusEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
            var integrationTypesEnumList = ((IntegrationEnum[])Enum.GetValues(typeof(IntegrationEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();

            currentCardModel.CurrentCardTypes = EnumUtilities.ConvertEnumToSelectListItem(currentCardTypeEnumList);
            currentCardModel.CompanyTypes = EnumUtilities.ConvertEnumToSelectListItem(CompanyTypeEnumList);
            currentCardModel.CurrentCardStatues = EnumUtilities.ConvertEnumToSelectListItem(currentCardStatuesEnumList);
            currentCardModel.IntegrationTypes = EnumUtilities.ConvertEnumToSelectListItem(integrationTypesEnumList);

            var currentCard = await _unitOfWork.CurrentCard.GetAsync(c => c.Id == id);

            currentCardModel.Id = currentCard.Id;
            currentCardModel.ErpId = currentCard.ErpId;
            currentCardModel.IdentificationNumber = currentCard.IdentificationNumber;
            currentCardModel.Surname = currentCard.Surname;
            currentCardModel.Name = currentCard.Name;
            currentCardModel.TaxNumber = currentCard.TaxNumber;
            currentCardModel.Status = currentCard.Status;
            currentCardModel.CommercialTitle = currentCard.CommercialTitle;
            currentCardModel.CompanyType = currentCard.CompanyType;
            currentCardModel.CurrentCode = currentCard.CurrentCode;
            currentCardModel.GSM = currentCard.GSM;
            currentCardModel.Description = currentCard.Description;
            currentCardModel.SpecialArea1 = currentCard.SpecialArea1;
            currentCardModel.SpecialArea2 = currentCard.SpecialArea2;
            currentCardModel.SpecialArea3 = currentCard.SpecialArea3;
            currentCardModel.SpecialArea4 = currentCard.SpecialArea4;
            currentCardModel.SpecialArea5 = currentCard.SpecialArea5;


            return View(currentCardModel);
        }
        #endregion
    }
}
