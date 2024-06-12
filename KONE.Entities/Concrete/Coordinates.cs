
using KONE.Shared.Entities.Abstract;

namespace KONE.Entities.Concrete
{
    public class Coordinates : EntityBase, IEntity
    {
        public bool IsMultiPolygon { get; set; } = false;
        public int Part { get; set; }
        public string EntitiesName { get; set; }
        public string EntitiesId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
