using KONE.Entities.Concrete;
using KONE.Shared.Data.Concrete.EntitiesFramework;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Repositories
{
    public class AddressesRepository : EfEntityRepositoryBase<Address>, IAddressesRepository
    {
        public AddressesRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
