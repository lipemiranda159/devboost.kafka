using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace ApiProducer
{
    public class Producer : KafkaProviderBase, IProducer
    {
        public Producer(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<DeliveryResult<Null, string>> SendData(string topic, string message)
        {
            var bootstrapServers = "kafka:29092";
            var nomeTopic = topic;


            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            return await producer.ProduceAsync(
                nomeTopic,
                new Message<Null, string>
                { Value = message });
        }
    }
}
