using System;
using MassTransit;
namespace UserSystem.Infrastructure
{
    public class BusInitializer
    {
        public static IBusControl CreateBus()
        {

            return Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                 {
                     h.Username("guest");
                     h.Password("guest");
                 });
            });
        }
    }
}
