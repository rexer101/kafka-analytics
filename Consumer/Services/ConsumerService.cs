using System.Text.Json;
using Confluent.Kafka;
using Consumer.Infrastruture;

namespace Consumer.Services;

 public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly RawDataService _rawDataService;
        private readonly OverSpeedingService _overSpeedingService;
        public ConsumerService(IConfiguration configuration,
        OverSpeedingService overSpeedingService, RawDataService rawDataService)
        {
            _overSpeedingService = overSpeedingService;
            _rawDataService = rawDataService;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = "CoreConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("trucks");

            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessKafkaMessage(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(4), stoppingToken);
            }

            _consumer.Close();
        }

        public async Task ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                var message = consumeResult.Message.Value;

                var rawdata = JsonSerializer.Deserialize<RawModel>(message);
                var data = JsonSerializer.Deserialize<OverSpeedingModel>(message);
                if(rawdata != null)
                    await _rawDataService.CreateAsync(rawdata);
                if(data != null && data.speed > 120)
                    await _overSpeedingService.CreateAsync(data);
            }
            catch (Exception ex)
            {
                
            }
        }
    }