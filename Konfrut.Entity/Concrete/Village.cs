using Konfrut.Shared.Entities.Abstract;

namespace KONE.Entity.Concrete
{
    public class Village : EntityBase, IEntity
    {
        public District District { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PropertyId { get; set; }
    }
}
