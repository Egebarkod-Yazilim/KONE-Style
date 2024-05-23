using Konfrut.Shared.Entities.Abstract;

namespace KONE.Entity.Concrete
{
    public class Province : EntityBase, IEntity
    {
        public string Name { get; set; }
        public int PropertyId { get; set; }
    }
}
