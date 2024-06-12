using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class Country : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string SpecialCode { get; set; }

        public ICollection<Province>? Provinces { get; set; }
        public ICollection<CurrentCard>? CurrentCards { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
