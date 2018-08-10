using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace ShoppingCart.EventFeed
{
    public class EventStore : IEventStore
    {
        private string connectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=ShoppingCart;IntegratedSecurity=True";

        private const string writeEventSql =
            @"insert into EventStore(Name, OccurredAt, Content) values
              (@Name, @OccurredAt, @Content)";

        private static long currentSequenceNumber = 0;
        private static readonly IList<Event> database = new List<Event>();

        public async Task<IEnumerable<Event>> GetEvents(
            long firstEventSequenceNumber,
            long lastEventSequenceNumber)
            => database
                .Where(e =>
                    e.SequenceNumber >= firstEventSequenceNumber &&
                    e.SequenceNumber <= lastEventSequenceNumber)
                .OrderBy(e => e.SequenceNumber);

        public Task Raise(string eventName, object content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.ExecuteAsync(writeEventSql, new
                {
                    Name = eventName,
                    OccurredAt = DateTimeOffset.Now,
                    Content = jsonContent
                });
            }
        }
    }
}
