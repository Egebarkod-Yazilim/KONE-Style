using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;


namespace KONE.DataAccess.KONE.Repositories
{
    public class VillageRepository : EfEntityRepositoryBase<Village>, IVillageRepository
    {
        public VillageRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
