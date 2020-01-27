using CsvHelper.Configuration.Attributes;

namespace Dingg_Databuilder.Models
{
    public class City
    {
        [Name("name")]
        public string Name { get; set; }
    }
}