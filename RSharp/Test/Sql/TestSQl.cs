using RSharp.API.SQL;
using System;
using System.Threading.Tasks;

namespace RSharp.Test.Sql
{
    internal class TestSql
    {
        internal async Task Test()
        {
            TestDao testDao = new TestDao();
            string testString = await testDao.TestString();
            Console.WriteLine(testString);
        }
    }

    internal class TestDao : BaseDao
    {
        internal async Task<string> TestString()
        {
            string test = "Failed";
            await CreateTransaction(async transaction =>
            {
                await Select(transaction, async reader =>
                {
                    if (await reader.ReadAsync())
                    {
                        test = reader.ReadData<string>("username");
                    }
                }, "SELECT id, username, password FROM players WHERE username = 'Spreed' AND password = 'edvin1234'");
            });

            return test;
        }
    }
}
