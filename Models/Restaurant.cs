using CsvHelper.Configuration.Attributes;

namespace Dingg.Databuilder.Models
{
    public class Restaurant
    {
        [Name("name")]
        public string Name { get; set; }
        [Name("description")] 
        public string Description { get; set; }
        [Name("address")]
        public string Address { get; set; }
        [Name("city")]
        public string City { get; set; }
        [Name("phone")]
        public string Phone { get; set; }
        [Name("website")]
        public string Website { get; set; }
        [Name("tags")]
        public string Tags { get; set; } 
        [Name("workhours")]
        public string Workhours { get; set; }
        [Name("workhours_saturday")]
        public string WorkhoursSaturday { get; set; }
        [Name("workhours_sunday")]
        public string WorkhoursSunday { get; set; }
    }
}