using System.Globalization;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Linq;

namespace Dingg_Databuilder.Extractors
{
    /// <summary>
    /// A generic CSV (with header) extractor. Extracts the data into an array of Model.
    /// </summary>
    /// <typeparam name="Model">A CsvHelper model. The model should match the CSV header we are trying to parse.</typeparam>
    public class CSV<Model>
    {
        private string _path;

        public CSV(string path)
        {
            this._path = path;
        }

        public Model[] Extract()
        {
            // LEARNING: Using statement is an alternative to try-catch block. What it does it automatically disposes
            //           the Disposable object once the block is finished or the exception occured.
            using (var reader = new StreamReader(this._path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.PrepareHeaderForMatch = (string header, int idex) => header.ToLower();
                    return csv.GetRecords<Model>().ToArray();
                }
            }
        }
    }
}