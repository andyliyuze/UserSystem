using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
namespace UserSystem.Infrastructure
{
    public class BusInitializer
    {
        //public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
        //{
        //    Log4NetLogger.Use();

        //    var bus = Bus.Factory.CreateUsingRabbitMq ServiceBusFactory.New(x =>
        //    {
        //        x.UseRabbitMq();
        //        x.ReceiveFrom("rabbitmq://localhost/Loosely_" + queueName);
        //        moreInitialization(x);
        //    });

        //    return bus;
        //}


        public static IBusControl CreateBus()
        {

            return Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host("localhost","userSystem", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }

    }
}
