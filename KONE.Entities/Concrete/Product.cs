using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class Product : EntityBase, IEntity
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string UnitType { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
