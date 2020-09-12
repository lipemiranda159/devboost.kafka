using Microsoft.Extensions.Configuration;
using System;

namespace Utils
{
    public abstract class KafkaProviderBase
    {
        protected readonly string _kafkaConnection;
        public KafkaProviderBase(IConfiguration configuration)
        {
            _kafkaConnection = configuration.GetSection("KafkaConnection").Value;
        }
    }
}
