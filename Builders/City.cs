
using System;
using System.Threading.Tasks;
using Neo4j.Driver;
using Dingg_Databuilder.Models;

namespace Dingg_Databuilder.Builders
{
    public static partial class Builder
    {
        public static async Task<int> City(City[] cities)
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
            IAsyncSession session = driver.AsyncSession();
            try
            {
                foreach (var city in cities)
                {
                    IResultCursor cursor = await session.RunAsync(
                        $@"MERGE (n:City {{ name: '{city.Name}' }})
                        ON CREATE SET n += {{ name: '{city.Name}' }}
                        ON MATCH SET n += {{ name: '{city.Name}' }}");
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
