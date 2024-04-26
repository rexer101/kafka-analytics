using System.Text.Json;
using Confluent.Kafka;
using Consumer.Dto;
using Consumer.Infrastruture;
using Newtonsoft.Json;

namespace Consumer.Services;

 public class ConsumerService : BackgroundService, IHostedService
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
            await Task.Yield();
            _consumer.Subscribe("trucks");

            while (!stoppingToken.IsCancellationRequested)
            {
                ProcessKafkaMessage(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _consumer.Close();
        }

        public void ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                var message = consumeResult.Message.Value;

                var kafkaMessage = JsonConvert.DeserializeObject<Messages>(message);

                if(kafkaMessage != null)
                {
                    var rawData = new RawModel
                    {
                        car_id = kafkaMessage.car_id,
                        display_name = kafkaMessage.display_name,
                        latitude = kafkaMessage.latitude,
                        longitude = kafkaMessage.longitude,
                        power = kafkaMessage.power,
                        speed = kafkaMessage.speed,
                        heading = kafkaMessage.heading,
                        elevation = kafkaMessage.elevation,
                        windows_open = kafkaMessage.windows_open,
                        is_user_present = kafkaMessage.is_user_present,
                        is_climate_on = kafkaMessage.is_climate_on,
                        inside_temp = kafkaMessage.inside_temp,
                        outside_temp = kafkaMessage.outside_temp,
                        odometer = kafkaMessage.odometer,
                        battery_level = kafkaMessage.battery_level,
                        timestamp = kafkaMessage.timestamp
                    };
                    _rawDataService.CreateAsync(rawData).WaitAsync(stoppingToken);

                    if(kafkaMessage.speed > 120)
                    {
                        var overSpeedData = new OverSpeedingModel
                        {
                            car_id = kafkaMessage.car_id,
                            speed = kafkaMessage.speed,
                            timestamp = kafkaMessage.timestamp
                        };
                        _overSpeedingService.CreateAsync(overSpeedData).WaitAsync(stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }