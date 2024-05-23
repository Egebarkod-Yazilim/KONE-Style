using KONE.Business.CBSAPI.Models;

namespace KONE.Business.CBSAPI
{
    public interface ICbsApiService
    {
        Task<DistrictReturnModel.Root> GetDistrictList(int provinceId);
        Task<ProvinceReturnModel.Root> GetProvinceList();
    }
}
