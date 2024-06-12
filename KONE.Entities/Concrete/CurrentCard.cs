using KONE.Shared.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using static KONE.Shared.Utils.Results.ComplexTypes.EnumVariables;

namespace KONE.Entities.Concrete
{
    public class CurrentCard : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CommercialTitle { get; set; }
        public string Description { get; set; }
        public CurrentCardStatusEnum Status { get; set; }
        public CurrentCardTypeEnum Type { get; set; }
        public CompanyTypeEnum CompanyType { get; set; }

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
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? VillageId { get; set; }

        // Navigation properties
        public Country? Country { get; set; }
        public Province? Province { get; set; }
        public District? District { get; set; }
        public Village? Village { get; set; }

        public string SpecialArea1 { get; set; }
        public string SpecialArea2 { get; set; }
        public string SpecialArea3 { get; set; }
        public string SpecialArea4 { get; set; }
        public string SpecialArea5 { get; set; }

        public IntegrationEnum IntegrationType { get; set; }

        public ICollection<CurrentCardLandName> CurrentCardLandNames { get; set; }
        public ICollection<CurrentCardAddressMapping> CurrentCardAddressMappings { get; set; }
    }
}
