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