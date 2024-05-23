using KONE.Entity.Concrete;
using Konfrut.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.Konfrut.Repositories
{
    public class CoordinatesRepository : EfEntityRepositoryBase<Coordinates>, ICoordinatesRepository
    {
        public CoordinatesRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
