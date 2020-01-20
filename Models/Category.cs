using CsvHelper.Configuration.Attributes;

namespace Dingg_Databuilder.Models
{
    public class Category
    {
        [Name("category")]
        public string Name { get; set; }
        [Name("popularity")]
        public int Popularity { get; set; }
    }
}