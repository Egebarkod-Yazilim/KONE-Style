namespace KONE.Business.CBSAPI.Models
{
    public class ProvinceReturnModel
    {
        public class Crs
        {
            public string type { get; set; }
            public Properties properties { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class PolygonModel
        {
            public string Type { get; set; }
            public List<List<List<double>>> Coordinates { get; set; }
        }

        public class MultiPolygonModel
        {
            public string Type { get; set; }
            public List<List<List<List<double>>>> Coordinates { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public List<List<List<object>>> coordinates { get; set; }
        }

        public class Properties
        {
            public string text { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Root
        {
            public List<Feature> features { get; set; }
            public string type { get; set; }
            public Crs crs { get; set; }
        }
    }
}
