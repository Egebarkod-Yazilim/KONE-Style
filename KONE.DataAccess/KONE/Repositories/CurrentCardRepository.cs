using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class CurrentCardRepository : EfEntityRepositoryBase<CurrentCard>, ICurrentCardsRepository
    {
        public CurrentCardRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
