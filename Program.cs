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
        static async Task<int> Build()
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
            IAsyncSession session = driver.AsyncSession();
            try
            {
                IResultCursor cursor = await session.RunAsync("CREATE (n:Category) RETURN n");
                await cursor.ConsumeAsync();
            }
            finally
            {
                await session.CloseAsync();
            }

            //...
            await driver.CloseAsync();
            Console.WriteLine("Built :)");
            return 0;
        }
        static void Main(string[] args)
        {
            // Unsure why im using async, when im literally blocking the thread, no point really
            // Emojis in code because why not. Git Bash renders it correctly. Quite funny actually.
            Console.WriteLine("👌 Dingg Databuilder 👌");

            Console.WriteLine("Loading categories");
            var CategoryCSV = new Extractors.CSV.Category(".\\Data\\Category.csv");
            CategoryCSV.Extract();

            //Build().Wait();
            // TODO: Make a model for importing i guess
            // Read categories from csv
            // For Category in Categories
            // execute cypher stuff :)
        }
    }
}
