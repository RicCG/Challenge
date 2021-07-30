using System;
using System.Collections.Generic;

namespace DataModel
{
    public class Citie
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ascii { get; set; }
        public string alt_name { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string feat_class { get; set; }
        public int population { get; set; }
        public int distance { get; set; }
        public double score { get; set; }

    }

    public class Suggestions { 
        public List<object> suggestions { get; set; }
    }
}
