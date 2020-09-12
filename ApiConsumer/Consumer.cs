using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace ApiConsumer
{
    public class Consumer : KafkaProviderBase,IConsumer
    {
        private readonly List<string> _messages;
        public Consumer(IConfiguration configuration) : base(configuration)
        {
            _messages = new List<string>();
        }

        public List<string> GetMessages()
        {
            return _messages;
        }


        private void StartServer(CancellationToken stopingToken, string topic)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
            logger.Information("Testando o consumo de mensagens com Kafka");


            string bootstrapServers = "kafka:29092";
            string nomeTopic = topic;

            logger.Information($"BootstrapServers = {bootstrapServers}");
            logger.Information($"Topic = {nomeTopic}");

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"{nomeTopic}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(nomeTopic);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        _messages.Add(cr.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                    logger.Warning("Cancelada a execução do Consumer...");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }


        public Task ExecuteAsync(CancellationToken stopingToken, string topic)
        {
            Task.Run(() => StartServer(stopingToken, topic));
            return Task.CompletedTask;
        }
    }

}
