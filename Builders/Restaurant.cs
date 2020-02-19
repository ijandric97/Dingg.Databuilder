
using System;
using System.Threading.Tasks;
using Neo4j.Driver;
using Dingg.Databuilder.Models;

namespace Dingg.Databuilder.Builders
{
    public static partial class Builder
    {
        public static async Task<int> Restaurant(Restaurant[] restaurants)
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test"));
            IAsyncSession session = driver.AsyncSession();
            try
            {
                foreach (var restaurant in restaurants)
                {
                    //Name;Description;Address;City;Phone;Website;
                    var query = $@"MERGE (n:Restaurant {{ name: '{restaurant.Name}' }})
                        ON CREATE SET n += {{ name: '{restaurant.Name}', description: '{restaurant.Description}', 
                            address: '{restaurant.Address}', phone: '{restaurant.Phone}', website: '{restaurant.Website}',
                            work_h: '{restaurant.Workhours}', saturday_h: '{restaurant.WorkhoursSaturday}',
                            sunday_h: '{restaurant.WorkhoursSunday}' }}
                        ON MATCH SET n += {{ name: '{restaurant.Name}', description: '{restaurant.Description}', 
                            address: '{restaurant.Address}', phone: '{restaurant.Phone}', website: '{restaurant.Website}',
                            work_h: '{restaurant.Workhours}', saturday_h: '{restaurant.WorkhoursSaturday}',
                            sunday_h: '{restaurant.WorkhoursSunday}' }}
                        CREATE UNIQUE ";
                    //Add tags
                    var i = 0;
                    foreach (var tag in restaurant.Tags.Split(","))
                    {
                        query = query + $"(n)-[:TAG]->(t{i}:Tag {{ name: '{tag}' }}), ";
                        i++;
                    }
                    query = query.Substring(0, query.Length - 2);
                    IResultCursor cursor = await session.RunAsync(query);
                    await cursor.ConsumeAsync();

                    // Connect to mah city
                    query = $@"MATCH (c:City {{ name: '{restaurant.City}'}}), (n:Restaurant {{ name: '{restaurant.Name}' }}) 
                    CREATE UNIQUE (c)-[:RESTAURANT]->(n)";
                    cursor = await session.RunAsync(query);
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
