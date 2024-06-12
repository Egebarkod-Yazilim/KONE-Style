using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class Village : EntityBase, IEntity
    {
        public District District { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PropertyId { get; set; }
    }
}
