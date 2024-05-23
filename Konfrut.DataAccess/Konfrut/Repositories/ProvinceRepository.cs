using KONE.Entity.Concrete;
using Konfrut.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.Konfrut.Repositories
{
    public class ProvinceRepository : EfEntityRepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
