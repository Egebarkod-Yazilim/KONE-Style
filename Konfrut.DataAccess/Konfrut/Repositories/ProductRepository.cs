using Konfrut.Entity.Concrete;
using Konfrut.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Konfrut.DataAccess.Konfrut.Repositories
{
    public class ProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
