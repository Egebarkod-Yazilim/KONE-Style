using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class CurrentCardAddressMappingsRepository : EfEntityRepositoryBase<CurrentCardAddressMapping>, ICurrentCardAddressMappingsRepository
    {
        public CurrentCardAddressMappingsRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
