using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProducer
{
    public interface IProducer
    {
        Task<DeliveryResult<Null, string>> SendData(string topic, string message);
    }
}
