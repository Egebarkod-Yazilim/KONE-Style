using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    internal class CurrentCardLandNameRepository : EfEntityRepositoryBase<CurrentCardLandName>, ICurrentCardLandNameRepository
    {
        public CurrentCardLandNameRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
