using KONE.Entities.Concrete;
using KONE.Shared.Utilities.Results.Abstract;
using KONE.Shared.Utilities.Results.Concrete;
using PagedList;

namespace KONE.Business.Services.Abstract
{
    public interface IProductService
    {
        Task<DataResult<Product>> AddOrUpdateAsync(Product product);
        Task<DataResult<Product>> GetByIdAsync(int id);
        Task<DataResult<Product>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<IPagedList<Product>> GetPagedListAsync(int pageNumber, int pageSize);
        Task<IResult> HardDeleteAsync(int id);
        Task<DataResult<Product>> ChangeEntitiesStatus(int id);
    }
}
