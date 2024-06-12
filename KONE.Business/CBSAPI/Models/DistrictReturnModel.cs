namespace KONE.Business.CBSAPI.Models
{
    public class DistrictReturnModel
    {
        public class Root
        {
            public Root()
            {
                features = new List<Feature>();
            }
            public List<Feature> features { get; set; }
            public string type { get; set; }
            public Crs crs { get; set; }
        }

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

        public class Geometry
        {
            public string type { get; set; }
            public List<List<List<object>>> coordinates { get; set; }
        }

        public class Geometry2
        {
            public string type { get; set; }
            public List<List<List<double>>> coordinates { get; set; }
        }

        public class Geometry3
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
    }
}
