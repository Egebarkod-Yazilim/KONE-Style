using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class CurrentCardAddressMapping : EntityBase, IEntity
    {
        public int CurrentCardId { get; set; }
        public CurrentCard CurrentCard { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int CurrentCardLandNameId { get; set; }
        public CurrentCardLandName CurrentCardLandName { get; set; }
    }
}
