using KONE.Business.Utilities;
using KONE.DataAccess.KONE.Abstract;
using KONE.Entities.Concrete;
using KONE.Entities.Dtos.CurrentCard;
using KONE.Shared.Utilities.Results.ComplexTypes;
using KONE.Shared.Utils.Results.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static KONE.Shared.Utils.Results.ComplexTypes.EnumVariables;

namespace KONE.WebUI.Controllers
{
    public class CurrentCardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrentCardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentCards()
        {
            try
            {
                var currentCards = await _unitOfWork.CurrentCard.GetAllPagedAsync(1, int.MaxValue);
                var countOfCurrentCards = await _unitOfWork.CurrentCard.CountAsync();
                var currentCardsList = currentCards.ToList();
                return Json(new { data = currentCardsList, draw = 1, recordsTotal = countOfCurrentCards, recordsFiltered = currentCards.Count });
            }
            catch (Exception ex)
            {
                return Json(new { data = "", draw = 1, recordsTotal = 0, recordsFiltered = 0 });
            }

        }
        [HttpGet]
        public async Task<IActionResult> CurrentLandList(int id)
        {
            var currentCard = await _unitOfWork.CurrentCard.GetAsync(c => c.Id == id);
            return View(new AddOrUpdateCurrentCard()
            {
                Id = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdateCurrentCard(int id)
        {
            if (id == 0)
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
                return View(currentCardModel);
            }
            else
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
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCurrentCard(AddOrUpdateCurrentCard model)
        {
            try
            {

                if (model.Id == 0)
                {
                    var currentCardModel = new AddOrUpdateCurrentCard();

                    if (model != null)
                        currentCardModel = model;

                    var currentCardTypeEnumList = ((CurrentCardTypeEnum[])Enum.GetValues(typeof(CurrentCardTypeEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
                    var CompanyTypeEnumList = ((CompanyTypeEnum[])Enum.GetValues(typeof(CompanyTypeEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
                    var currentCardStatuesEnumList = ((CurrentCardStatusEnum[])Enum.GetValues(typeof(CurrentCardStatusEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
                    var integrationTypesEnumList = ((IntegrationEnum[])Enum.GetValues(typeof(IntegrationEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();

                    currentCardModel.CurrentCardTypes = EnumUtilities.ConvertEnumToSelectListItem(currentCardTypeEnumList, model.Type.ToString());
                    currentCardModel.CompanyTypes = EnumUtilities.ConvertEnumToSelectListItem(CompanyTypeEnumList, model.CompanyType.ToString());
                    currentCardModel.CurrentCardStatues = EnumUtilities.ConvertEnumToSelectListItem(currentCardStatuesEnumList, model.Status.ToString());
                    currentCardModel.IntegrationTypes = EnumUtilities.ConvertEnumToSelectListItem(integrationTypesEnumList, model.IntegrationType.ToString());

                    var newCurrentCard = new CurrentCard()
                    {
                        CommercialTitle = model.CommercialTitle,
                        Description = model.Description,
                        Email = model.Email,
                        Name = model.Name,
                        GSM = model.GSM,
                        IntegrationType = model.IntegrationType,
                        Note = "",
                        SpecialArea1 = model.SpecialArea1,
                        SpecialArea2 = model.SpecialArea2,
                        SpecialArea3 = model.SpecialArea3,
                        SpecialArea4 = model.SpecialArea4,
                        SpecialArea5 = model.SpecialArea5,
                        IdentificationNumber = model.IdentificationNumber,
                        TaxNumber = model.TaxNumber,
                        TaxOffice = model.TaxOffice,
                        Status = model.Status,
                        Surname = model.Surname,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        CompanyType = model.CompanyType,
                        CreatedByName = "SYSTEM",
                        ModifiedByName = "SYSTEM",
                        CurrentCode = model.CurrentCode,
                        ErpId = model.ErpId,
                    };

                    await _unitOfWork.CurrentCard.AddAsync(newCurrentCard);
                    await _unitOfWork.SaveAsync();

                    return View(model);
                }
                else
                {
                    var currentCardModel = new AddOrUpdateCurrentCard();

                    if (model != null)
                        currentCardModel = model;

                    var currentCardTypeEnumList = ((CurrentCardTypeEnum[])Enum.GetValues(typeof(CurrentCardTypeEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
                    var CompanyTypeEnumList = ((CompanyTypeEnum[])Enum.GetValues(typeof(CompanyTypeEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
                    var currentCardStatuesEnumList = ((CurrentCardStatusEnum[])Enum.GetValues(typeof(CurrentCardStatusEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();
                    var integrationTypesEnumList = ((IntegrationEnum[])Enum.GetValues(typeof(IntegrationEnum))).Select(c => new EnumListItem() { Id = (int)c, Name = c.ToString() }).ToList();

                    currentCardModel.CurrentCardTypes = EnumUtilities.ConvertEnumToSelectListItem(currentCardTypeEnumList, model.Type.ToString());
                    currentCardModel.CompanyTypes = EnumUtilities.ConvertEnumToSelectListItem(CompanyTypeEnumList, model.CompanyType.ToString());
                    currentCardModel.CurrentCardStatues = EnumUtilities.ConvertEnumToSelectListItem(currentCardStatuesEnumList, model.Status.ToString());
                    currentCardModel.IntegrationTypes = EnumUtilities.ConvertEnumToSelectListItem(integrationTypesEnumList, model.IntegrationType.ToString());

                    var newCurrentCard = new CurrentCard()
                    {
                        CommercialTitle = model.CommercialTitle,
                        Description = model.Description,
                        Email = model.Email,
                        Name = model.Name,
                        GSM = model.GSM,
                        IntegrationType = model.IntegrationType,
                        Note = "",
                        SpecialArea1 = model.SpecialArea1,
                        SpecialArea2 = model.SpecialArea2,
                        SpecialArea3 = model.SpecialArea3,
                        SpecialArea4 = model.SpecialArea4,
                        SpecialArea5 = model.SpecialArea5,
                        IdentificationNumber = model.IdentificationNumber,
                        TaxNumber = model.TaxNumber,
                        TaxOffice = model.TaxOffice,
                        Status = model.Status,
                        Surname = model.Surname,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        CompanyType = model.CompanyType,
                        CreatedByName = "SYSTEM",
                        ModifiedByName = "SYSTEM",
                        CurrentCode = model.CurrentCode,
                        ErpId = model.ErpId,
                    };

                    await _unitOfWork.CurrentCard.AddAsync(newCurrentCard);
                    await _unitOfWork.SaveAsync();

                    return View(model);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> CurrentCardAddresses(int id)
        {
            var currentCard = new AddOrUpdateCurrentCard();
            currentCard.Id = id;
            return View(currentCard);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdateCurrentCardAddress(int currentcardid, int addressid, int landnameid = 0)
        {
            if (currentcardid != 0 && addressid == 0)
            {
                var address = new AddOrUpdateCurrentCardAddress();
                address.CurrentCardId = currentcardid;
                address.LandNameId = landnameid;

                var countries = await _unitOfWork.Countries.GetAllAsync();
                var provinces = await _unitOfWork.Province.GetAllAsync();
                var districts = await _unitOfWork.District.GetAllAsync();


                foreach (var country in countries)
                {
                    if (country.Name == "Turkey")
                    {

                        address.Countries.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = country.Name,
                            Value = country.Id.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        address.Countries.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = country.Name,
                            Value = country.Id.ToString(),
                        });
                    }
                }

                foreach (var province in provinces)
                {
                    address.Provinces.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                    {
                        Text = province.Name,
                        Value = province.PropertyId.ToString()
                    });
                }

                return View(address);
            }
            else if (currentcardid != 0)
            {
                var currentCardAddress = await _unitOfWork.Addresses.GetAsync(c => c.Id == addressid, c => c.Province, c => c.District, c => c.Village);

                var address = new AddOrUpdateCurrentCardAddress();
                address.CurrentCardId = currentcardid;


                var countries = await _unitOfWork.Countries.GetAllAsync();
                var provinces = await _unitOfWork.Province.GetAllAsync(c => c.Id == currentCardAddress.ProvinceId);
                var districts = await _unitOfWork.District.GetAllAsync(c => c.Id == currentCardAddress.DistrictId);
                var villages = await _unitOfWork.Village.GetAllAsync(c => c.Id == currentCardAddress.VillageId);

                address.Sheet = currentCardAddress.Sheet;
                address.Email = currentCardAddress.Email;
                address.Location = currentCardAddress.Location;
                address.FirstName = currentCardAddress.FirstName;
                address.LastName = currentCardAddress.LastName;
                address.AddressDetail = currentCardAddress.AddressDetail;
                address.AddressId = currentCardAddress.Id;
                address.BlockIsland = currentCardAddress.BlockIsland;
                address.CompanyName = currentCardAddress.CompanyName;
                address.CountryId = currentCardAddress.CountryId;
                address.DistrictId = currentCardAddress.DistrictId;
                address.VillageId = currentCardAddress.VillageId;
                address.ProvinceId = currentCardAddress.ProvinceId;
                address.PhoneNumber = currentCardAddress.PhoneNumber;
                address.Email = currentCardAddress.Email;
                address.PhoneNumber = currentCardAddress.PhoneNumber;
                address.Title = currentCardAddress.Title;
                address.SatelliteViewImage = currentCardAddress.SatelliteViewImage;
                address.FloorType = currentCardAddress.FloorType;
                address.Parcel = currentCardAddress.Parcel;
                address.Property = currentCardAddress.Property;
                address.Sheet = currentCardAddress.Sheet;
                address.TitleDeedArea = currentCardAddress.TitleDeedArea;
                address.SpecialArea1 = currentCardAddress.SpecialArea1;
                address.SpecialArea2 = currentCardAddress.SpecialArea2;
                address.SpecialArea3 = currentCardAddress.SpecialArea3;
                address.SpecialArea4 = currentCardAddress.SpecialArea4;
                address.SpecialArea5 = currentCardAddress.SpecialArea5;
                address.ZipCode = currentCardAddress.ZipCode;

                foreach (var country in countries)
                {
                    if (country.Name == "Turkey")
                    {

                        address.Countries.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = country.Name,
                            Value = country.Id.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        address.Countries.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = country.Name,
                            Value = country.Id.ToString(),
                        });
                    }
                }
                foreach (var province in provinces)
                {
                    if (currentCardAddress.ProvinceId == province.Id)
                    {
                        address.Provinces.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = province.Name,
                            Value = province.PropertyId.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        address.Provinces.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = province.Name,
                            Value = province.PropertyId.ToString()
                        });
                    }

                }
                foreach (var district in districts)
                {
                    if (currentCardAddress.DistrictId == district.Id)
                    {
                        address.Districts.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = district.Name,
                            Value = district.PropertyId.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        address.Districts.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = district.Name,
                            Value = district.PropertyId.ToString()
                        });
                    }

                }
                foreach (var village in villages)
                {
                    if (currentCardAddress.VillageId == village.Id)
                    {
                        address.Villages.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = village.Name,
                            Value = village.PropertyId.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        address.Villages.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                        {
                            Text = village.Name,
                            Value = village.PropertyId.ToString()
                        });
                    }

                }

                return View(address);
            }

            return View(new Address());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCurrentCardAddress(AddOrUpdateCurrentCardAddress model)
        {
            if (model.AddressId != 0)
            {
                var currentAddress = await _unitOfWork.Addresses.GetAsync(c => c.Id == model.AddressId);
                var villageEntityId = await _unitOfWork.Village.GetAsync(c => c.PropertyId == model.VillageId);
                var provinceEntityId = await _unitOfWork.Province.GetAsync(c => c.PropertyId == model.ProvinceId);
                var districtEntityId = await _unitOfWork.District.GetAsync(c => c.PropertyId == model.DistrictId);
                //Guncelle

                currentAddress.Id = currentAddress.Id;
                currentAddress.CountryId = model.CountryId;
                currentAddress.DistrictId = districtEntityId.Id;
                currentAddress.ProvinceId = provinceEntityId.Id;
                currentAddress.VillageId = villageEntityId.Id;
                currentAddress.AddressDetail = model.AddressDetail;
                currentAddress.IsDeleted = false;
                currentAddress.IsActive = true;
                currentAddress.CreatedDate = DateTime.Now;
                currentAddress.ModifiedDate = DateTime.Now;
                currentAddress.TitleDeedArea = model.TitleDeedArea;
                currentAddress.BlockIsland = model.BlockIsland;
                currentAddress.CompanyName = model.CompanyName;
                currentAddress.CreatedByName = "SYSTEM";
                currentAddress.Note = "SYSTEM";
                currentAddress.ModifiedByName = "SYSTEM";
                currentAddress.Email = model.Email;
                currentAddress.LastName = model.LastName;
                currentAddress.FirstName = model.FirstName;
                currentAddress.PhoneNumber = model.PhoneNumber;
                currentAddress.FloorType = model.FloorType;
                currentAddress.Location = model.Location;
                currentAddress.Parcel = model.Parcel;
                currentAddress.Property = model.Property;
                currentAddress.Sheet = model.Sheet;
                currentAddress.SpecialArea1 = model.SpecialArea1;
                currentAddress.SpecialArea2 = model.SpecialArea2;
                currentAddress.SpecialArea3 = model.SpecialArea3;
                currentAddress.SpecialArea4 = model.SpecialArea4;
                currentAddress.SpecialArea5 = model.SpecialArea5;
                currentAddress.Title = model.Title;
                currentAddress.ZipCode = model.ZipCode;
                currentAddress.SatelliteViewImage = "";

                await _unitOfWork.Addresses.UpdateAsync(currentAddress);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("AddOrUpdateCurrentCardAddress", new { id = model.CurrentCardId });
            }
            else
            {
                var villageEntityId = await _unitOfWork.Village.GetAsync(c => c.PropertyId == model.VillageId);
                var provinceEntityId = await _unitOfWork.Province.GetAsync(c => c.PropertyId == model.ProvinceId);
                var districtEntityId = await _unitOfWork.District.GetAsync(c => c.PropertyId == model.DistrictId);

                //Ekle
                var newAddress = new Address()
                {
                    CountryId = model.CountryId,
                    DistrictId = districtEntityId.Id,
                    ProvinceId = provinceEntityId.Id,
                    VillageId = villageEntityId.Id,
                    AddressDetail = model.AddressDetail,
                    IsDeleted = false,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    TitleDeedArea = model.TitleDeedArea,
                    BlockIsland = model.BlockIsland,
                    CompanyName = model.CompanyName,
                    CreatedByName = "SYSTEM",
                    Note = "SYSTEM",
                    ModifiedByName = "SYSTEM",
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    PhoneNumber = model.PhoneNumber,
                    FloorType = model.FloorType,
                    Location = model.Location,
                    Parcel = model.Parcel,
                    Property = model.Property,
                    Sheet = model.Sheet,
                    SpecialArea1 = model.SpecialArea1,
                    SpecialArea2 = model.SpecialArea2,
                    SpecialArea3 = model.SpecialArea3,
                    SpecialArea4 = model.SpecialArea4,
                    SpecialArea5 = model.SpecialArea5,
                    Title = model.Title,
                    ZipCode = model.ZipCode,
                    SatelliteViewImage = "",
                };

                await _unitOfWork.Addresses.AddAsync(newAddress);
                await _unitOfWork.SaveAsync();

                var newAddressMapping = new CurrentCardAddressMapping()
                {
                    AddressId = newAddress.Id,
                    CurrentCardId = model.CurrentCardId,
                    CurrentCardLandNameId = model.LandNameId,
                    CreatedByName = "SYSTEM",
                    ModifiedByName = "SYSTEM",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    ModifiedDate = DateTime.Now,
                };

                await _unitOfWork.CurrentCardAddressMapping.AddAsync(newAddressMapping);
                await _unitOfWork.SaveAsync();


                return RedirectToAction("AddOrUpdateCurrentCardAddress", new { currentcardid = model.CurrentCardId });
            }
        }


        [HttpGet]
        public async Task<IActionResult> FetchCurrentCardWithAddress(int currentCardId)
        {
            var currentcard = await _unitOfWork.CurrentCard.GetAsync(c => c.Id == currentCardId);
            return Json(new
            {
                resultStatus = ResultStatus.Success,
                messages = new List<string> { "Cari kartı başarıyla adres ile eşlendi!" },
                data = currentcard
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdateCurrentCardLandName(int id, int currentcardid)
        {
            var currendLandName = await _unitOfWork.CurrentCardLandName.GetAsync(c => c.Id == id);

            if (currendLandName != null)
                return PartialView("_AddOrUpdateCurrentCardLandName", currendLandName);
            else
                return PartialView("_AddOrUpdateCurrentCardLandName", new CurrentCardLandName()
                {
                    CurrentCardId = currentcardid
                });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCurrentCardLandName(CurrentCardLandName model)
        {
            try
            {
                if (model.Id != 0)
                {
                    //guncelle
                    var currentCardLandName = await _unitOfWork.CurrentCardLandName.GetAsync(c => c.Id == model.Id);
                    currentCardLandName.Description = model.Description;
                    currentCardLandName.Name = model.Name;
                    currentCardLandName.CurrentCardId = model.Id;
                    await _unitOfWork.CurrentCardLandName.UpdateAsync(currentCardLandName);
                    await _unitOfWork.SaveAsync();

                    return Json(new
                    {
                        resultStatus = ResultStatus.Success,
                        messages = new List<string>() { "Başarıyla güncellendi" },
                        data = currentCardLandName
                    });
                }
                else
                {
                    //ekle
                    var returnModel = await _unitOfWork.CurrentCardLandName.AddAsync(model);
                    await _unitOfWork.SaveAsync();
                    return Json(new
                    {
                        resultStatus = ResultStatus.Success,
                        messages = new List<string>() { "Başarıyla eklendi" },
                        data = returnModel
                    });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    resultStatus = ResultStatus.Error,
                    messages = new List<string>() { "Hatalı işlem." },
                    data = ""
                });
            }

        }

        [HttpGet]
        public async Task<IActionResult> CureendCardLandNameAddressList(int id, int currentcardid)
        {
            ViewBag.CurrentCardId = currentcardid;
            ViewBag.LandNameId = id;
            return View();
        }
    }
}
