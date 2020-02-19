using CsvHelper.Configuration.Attributes;

namespace Dingg.Databuilder.Models
{
    public class Category
    {
        [Name("name")]
        public string Name { get; set; }
        [Name("popularity")]
        public int Popularity { get; set; }
    }
}