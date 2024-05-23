using KONE.DataAccess.Konfrut.Repositories;
using Konfrut.DataAccess.Konfrut.Abstract;
using Konfrut.DataAccess.Konfrut.Repositories;

namespace Konfrut.DataAccess.Konfrut.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Repositories
        private readonly KonfrutContext _KonfrutContext;
        private readonly IProductRepository _productRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly ICoordinatesRepository _coordinatesRepository;
        #endregion

        #region Ctor
        public UnitOfWork(KonfrutContext context)
        {
            _KonfrutContext = context;
        }



        #endregion

        #region Implementations
        public IProductRepository Product => _productRepository ?? new ProductRepository(_KonfrutContext);

        public IDistrictRepository District => _districtRepository ?? new DistrictRepository(_KonfrutContext);

        public IProvinceRepository Province => _provinceRepository ?? new ProvinceRepository(_KonfrutContext);

        public ICoordinatesRepository Coordinates => _coordinatesRepository ?? new CoordinatesRepository(_KonfrutContext);

        public void Dispose()
        {
            _KonfrutContext.Dispose();
        }
        #endregion

        #region Methods
        public async ValueTask DisposeAsync()
        {
            await _KonfrutContext.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _KonfrutContext.SaveChangesAsync();
        }
        #endregion
    }
}
