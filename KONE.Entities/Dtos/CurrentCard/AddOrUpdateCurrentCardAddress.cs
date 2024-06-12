
using KONE.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KONE.Entities.Dtos.CurrentCard
{
    public class AddOrUpdateCurrentCardAddress
    {
        public AddOrUpdateCurrentCardAddress()
        {
            Provinces = new List<SelectListItem>();
            Districts = new List<SelectListItem>();
            Countries = new List<SelectListItem>();
            Villages = new List<SelectListItem>();
        }
        public int LandNameId { get; set; }
        public int CurrentCardId { get; set; }
        public int AddressId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }

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

        public string BlockIsland { get; set; }
        public string Parcel { get; set; }
        public string TitleDeedArea { get; set; }
        public string Property { get; set; }
        public string Location { get; set; }
        public string FloorType { get; set; }
        public string Sheet { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string SatelliteViewImage { get; set; }
        public string SpecialArea1 { get; set; }
        public string SpecialArea2 { get; set; }
        public string SpecialArea3 { get; set; }
        public string SpecialArea4 { get; set; }
        public string SpecialArea5 { get; set; }
    }
}
