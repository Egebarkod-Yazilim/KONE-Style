using KONE.DataAccess.Konfrut.Repositories;
using Konfrut.DataAccess.Konfrut.Repositories;

namespace Konfrut.DataAccess.Konfrut.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IProductRepository Product { get; }
        IDistrictRepository District { get; }
        IProvinceRepository Province { get; }
        IVillageRepository Village { get; }
        ICoordinatesRepository Coordinates { get; }
        Task<int> SaveAsync();
    }
}
