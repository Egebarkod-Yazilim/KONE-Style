using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class ProvinceRepository : EfEntityRepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
