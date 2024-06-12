using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class DistrictRepository : EfEntityRepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
