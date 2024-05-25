using KONE.Entity.Concrete;
using Konfrut.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.Konfrut.Repositories
{
    public class VillageRepository : EfEntityRepositoryBase<Village>, IVillageRepository
    {
        public VillageRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
