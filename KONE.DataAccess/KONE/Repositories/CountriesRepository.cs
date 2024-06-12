using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class CountriesRepository : EfEntityRepositoryBase<Country>, ICountriesRepository
    {
        public CountriesRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
