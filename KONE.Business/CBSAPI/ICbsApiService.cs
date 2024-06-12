using KONE.Business.CBSAPI.Models;

namespace KONE.Business.CBSAPI
{
    public interface ICbsApiService
    {
        Task<DistrictReturnModel.Root> GetDistrictList(int provinceId);
        Task<VillageReturnModel.Root> GetVillageList(int districtId);
        Task<ParcelReturnModel.Root> GetParcelList(int villageId, int land = 0, int parcel = 0);
        Task<ProvinceReturnModel.Root> GetProvinceList();
    }
}
