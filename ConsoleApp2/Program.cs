using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PUBLISH");

            var factory = new ConnectionFactory()
            {
                UserName = "user",
                Password = "fiap",
                HostName = "23.99.218.43"
            };

            var body = Encoding.UTF8.GetBytes("P1: Qual é a capital do Brasil?");

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        channel.BasicPublish(exchange: "",
                                            routingKey: "15net",
                                            basicProperties: null,
                                            body: body);


                        Console.WriteLine("Publicou " + i);

                        Thread.Sleep(1000);
                    }

                }
            }

            Console.ReadLine();
        }
    }
}
