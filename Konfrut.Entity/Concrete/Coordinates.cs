
using Konfrut.Shared.Entities.Abstract;

namespace KONE.Entity.Concrete
{
    public class Coordinates : EntityBase, IEntity
    {
        public bool IsMultiPolygon { get; set; } = false;
        public int Part { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
