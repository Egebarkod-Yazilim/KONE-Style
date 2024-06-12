using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class Province : EntityBase, IEntity
    {
        public string Name { get; set; }
        public int PropertyId { get; set; }
    }
}
