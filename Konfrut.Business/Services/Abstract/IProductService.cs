using Konfrut.Entity.Concrete;
using Konfrut.Shared.Utilities.Results.Abstract;
using Konfrut.Shared.Utilities.Results.Concrete;
using PagedList;

namespace Konfrut.Business.Services.Abstract
{
    public interface IProductService
    {
        Task<DataResult<Product>> AddOrUpdateAsync(Product product);
        Task<DataResult<Product>> GetByIdAsync(int id);
        Task<DataResult<Product>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<IPagedList<Product>> GetPagedListAsync(int pageNumber, int pageSize);
        Task<IResult> HardDeleteAsync(int id);
        Task<DataResult<Product>> ChangeEntityStatus(int id);
    }
}
