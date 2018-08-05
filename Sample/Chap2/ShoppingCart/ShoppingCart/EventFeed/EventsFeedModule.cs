using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace ShoppingCart.EventFeed
{
    public class EventsFeedModule : NancyModule
    {
        public EventsFeedModule(IEventStore eventStore) : base("/events")
        {
            Get("/", _ =>
            {
                long firstEventSequenceNumber, lastEventSequenceNumber;
                if (!long.TryParse(this.Request.Query.start.Value,
                    out firstEventSequenceNumber))
                    firstEventSequenceNumber = 0;

                if (!long.TryParse(this.Request.Query.end.Value,
                    out lastEventSequenceNumber))
                    lastEventSequenceNumber = long.MaxValue;

                //Returns the raw list of events.
                //Nancy takes care of serializing the
                //events into the response body.
                return
                    eventStore.GetEvents(
                        firstEventSequenceNumber,
                        lastEventSequenceNumber);
            });
        }
    }
}
