using System;
using System.ServiceProcess;
namespace LoyaltyProgramEventConsumer
{
    public class EventSubscriber
    {

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
