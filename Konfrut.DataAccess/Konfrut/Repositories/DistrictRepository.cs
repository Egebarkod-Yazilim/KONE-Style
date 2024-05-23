using KONE.Entity.Concrete;
using Konfrut.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.Konfrut.Repositories
{
    public class DistrictRepository : EfEntityRepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
