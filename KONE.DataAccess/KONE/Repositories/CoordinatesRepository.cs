using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class CoordinatesRepository : EfEntityRepositoryBase<Coordinates>, ICoordinatesRepository
    {
        public CoordinatesRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
