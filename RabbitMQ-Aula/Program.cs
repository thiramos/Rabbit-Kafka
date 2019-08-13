using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using Confluent.Kafka;
using System.Text;
using System.Diagnostics;

namespace RabbitMQ_Aula
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer Rabbit - Publish kafka");

            var factory = new ConnectionFactory()
            {
                UserName = "user",
                Password = "fiap",
                HostName = "23.99.218.43"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume("ReceberMsgResp", true, consumer);

                    Console.ReadLine();
                }
            }

        }

        private static async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var msg = Encoding.UTF8.GetString(e.Body);

                Debug.WriteLine("Msg recebida: " + msg);

                var config = new ProducerConfig();
                config.BootstrapServers = "23.99.218.43:9092";

                var builder = new ProducerBuilder<string, string>(config);

                using (var producer = builder.Build())
                {
                    string topic = "15netkafka";

                    var message = new Message<string, string>
                    {
                        Key = "Grupo1",
                        Value = msg
                    };

                    await producer.ProduceAsync(topic, message);
                }            }
            catch
            {
                Console.WriteLine("Erro na msg recebida!");
            }
        }
    }
}
