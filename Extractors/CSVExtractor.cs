using System.Globalization;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Linq;

namespace Dingg.Databuilder.Extractors
{
    /// <summary>
    /// A generic CSV (with header and ; delimiter) extractor. Extracts the data into an array of Model.
    /// </summary>
    /// <typeparam name="Model">A CsvHelper model. The model should match the CSV header we are trying to parse.</typeparam>
    public class CSV<Model>
    {
        /// <summary>
        /// The path to the CSV file on the disk
        /// </summary>
        private string _path;
        
        /// <summary>
        /// A generic CSV (with header and ; delimiter) extractor. Extracts the data into an array of Model.
        /// </summary>
        /// <param name="path">The path to the CSV file on the disk</param>
        public CSV(string path)
        {
            this._path = path;
        }

        /// <summary>
        /// Attempts to read the CSV (with header) and extract its content into an array of Model.
        /// </summary>
        /// <returns>Model[] - Extracted CSV data into the array of Model</returns>
        public Model[] Extract()
        {
            // LEARNING: Using statement is an alternative to try-catch block. What it does it automatically disposes
            //           the Disposable object once the block is finished or the exception occured.
            using (var reader = new StreamReader(this._path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.PrepareHeaderForMatch = (string header, int idex) => header.ToLower();
                    return csv.GetRecords<Model>().ToArray();
                }
            }
        }
    }
}