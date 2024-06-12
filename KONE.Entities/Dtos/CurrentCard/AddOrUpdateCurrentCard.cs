using KONE.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static KONE.Shared.Utils.Results.ComplexTypes.EnumVariables;

namespace KONE.Entities.Dtos.CurrentCard
{
    public class AddOrUpdateCurrentCard
    {
        public AddOrUpdateCurrentCard()
        {
            CurrentCardTypes = new List<SelectListItem>();
            CurrentCardStatues = new List<SelectListItem>();
            CompanyTypes = new List<SelectListItem>();
            Countries = new List<SelectListItem>();
            Provinces = new List<SelectListItem>();
            Districts = new List<SelectListItem>();
            Villages = new List<SelectListItem>();
            IntegrationTypes = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public int LandNameId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CommercialTitle { get; set; }
        public string Description { get; set; }
        public CurrentCardStatusEnum Status { get; set; }
        public CurrentCardTypeEnum Type { get; set; }
        public CompanyTypeEnum CompanyType { get; set; }

        public List<SelectListItem> CurrentCardStatues { get; set; }
        public List<SelectListItem> CurrentCardTypes { get; set; }
        public List<SelectListItem> CompanyTypes { get; set; }

        public string ErpId { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "GSM numarası 11 karakter uzunluğunda olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "GSM numarası sadece rakamlardan oluşmalıdır.")]
        public string GSM { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz email adresi.")]
        public string Email { get; set; }

        public string IdentificationNumber { get; set; }
        public string CurrentCode { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }

        // Foreign key properties
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int VillageId { get; set; }

        // Navigation properties
        public Country Country { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public Village Village { get; set; }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Provinces { get; set; }
        public List<SelectListItem> Districts { get; set; }
        public List<SelectListItem> Villages { get; set; }


        public string SpecialArea1 { get; set; }
        public string SpecialArea2 { get; set; }
        public string SpecialArea3 { get; set; }
        public string SpecialArea4 { get; set; }
        public string SpecialArea5 { get; set; }

        public IntegrationEnum IntegrationType { get; set; }
        public List<SelectListItem> IntegrationTypes { get; set; }

    }
}
