using System;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyProgramEventConsumer
{
    public class EventSubscriber
    {
        private readonly string loyaltyProgramHost;
        private long start = 0;
        private int chunkSize = 100;
        private readonly Timer timer;

        public EventSubscriber()
        {
            this.loyaltyProgramHost = loyaltyProgramHost;
            this.timer = new Timer(10 * 1000);
            this.timer.AutoReset = false;
            this.timer.Elapsed += (_, __) => SubscriptionCycleCallback().Wait();
        }

        private async Task SubscriptionCycleCallback()
        {
            var response = await ReadEvents().ConfigureAwait(false);
            if(response.StatusCode == HttpStatusCode.OK)
                HandleEvents(response.Content);
            this.timer.Start();
        }

        private async Task<HttpResponseMessage> ReadEvents()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress =
                    new Uri($"http://{this.loyaltyProgramHost}");
                var response = await httpClient.GetAsync(
                        $"/events/?start={this.start}&end={this.start + this.chunkSize}")
                    .ConfigureAwait(false);
                return response;
            }
        }

    public class Program : ServiceBase
    {
        private EventSubscriber subscriber;

        public static void Main(string[] args) => new Program().Main();

        public void Main()
        {
            // more to come
            Run(this);
        }
        protected override void OnStart(string[] args)
        {
            // more to come
        }
        protected override void OnStop()
        {
            // more to come
        }
    }
}
