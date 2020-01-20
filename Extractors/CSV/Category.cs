using System.Globalization;
using System.IO;
using CsvHelper;

namespace Dingg_Databuilder.Extractors.CSV
{
    public class Category
    {
        private string _path;

        public Category(string path)
        {
            this._path = path;
        }

        public void Extract()
        {
            // LEARNING: Using statement is an alternative to try-catch block. What it does it automatically disposes
            //           the Disposable object once the block is finished or the exception occured.
            using (var reader = new StreamReader(this._path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.PrepareHeaderForMatch = (string header, int idex) => header.ToLower();
                    var records = csv.GetRecords<Models.Category>();
                    return records;
                    /*foreach (var record in records)
                    {
                        // TODO: Remove this and add proper return for this shit :)
                        System.Console.WriteLine($"Name: {record.Name}, Popularity: {record.Popularity}");
                    }*/
                }
            }
        }
    }
}