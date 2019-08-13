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


            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        var body = Encoding.UTF8.GetBytes("P" + i + ": Qual é a capital do Brasil?");

                        channel.BasicPublish(exchange: "EnviarMensagem",
                                            routingKey: "",
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
