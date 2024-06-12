using KONE.DataAccess.KONE.Repositories;

namespace KONE.DataAccess.KONE.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IProductRepository Product { get; }
        IDistrictRepository District { get; }
        IProvinceRepository Province { get; }
        IVillageRepository Village { get; }
        ICoordinatesRepository Coordinates { get; }
        ICountriesRepository Countries { get; }
        ICurrentCardsRepository CurrentCard { get; }
        ICurrentCardAddressMappingsRepository CurrentCardAddressMapping { get; }
        IAddressesRepository Addresses { get; }
        ICurrentCardLandNameRepository CurrentCardLandName { get; }
        Task<int> SaveAsync();
    }
}
