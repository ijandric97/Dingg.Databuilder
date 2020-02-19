
using System;
using System.Threading.Tasks;
using Neo4j.Driver;
using Dingg.Databuilder.Models;

namespace Dingg.Databuilder.Builders
{
    public static partial class Builder
    {
        public static async Task<int> Category(Category[] categories)
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
            IAsyncSession session = driver.AsyncSession();
            try
            {
                foreach (var category in categories)
                {
                    IResultCursor cursor = await session.RunAsync(
                        $@"MERGE (n:Category {{ name: '{category.Name}' }})
                        ON CREATE SET n += {{ name: '{category.Name}', popularity: {category.Popularity} }}
                        ON MATCH SET n += {{ name: '{category.Name}', popularity: {category.Popularity} }}");
                    await cursor.ConsumeAsync();
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            await driver.CloseAsync();
            return 0;
        }
    }
}
