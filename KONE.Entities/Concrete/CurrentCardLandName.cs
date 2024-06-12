using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class CurrentCardLandName : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentCardId { get; set; }
        public CurrentCard CurrentCard { get; set; }
        public ICollection<CurrentCardAddressMapping> CurrentCardAddressMappings { get; set; }
    }
}
