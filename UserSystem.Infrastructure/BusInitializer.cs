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
