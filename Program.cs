using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Neo4j.Driver;

namespace Dingg_Databuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            // Unsure why im using async, when im literally blocking the thread, no point really
            // Emojis in code because why not. Git Bash renders it correctly. Quite funny actually.
            Console.WriteLine("👌 Dingg Databuilder 👌");

            Console.WriteLine("Loading categories");
            var CategoryCSV = new Extractors.CSV<Models.Category>(".\\Data\\Category.csv");
            
            //TESTING; TODO
            foreach (var record in CategoryCSV.Extract()) {
                Console.WriteLine(record.Name);
            }
            
            


            //Build().Wait();
            // TODO: Make a model for importing i guess
            // Read categories from csv
            // For Category in Categories
            // execute cypher stuff :)
        }
    }
}
