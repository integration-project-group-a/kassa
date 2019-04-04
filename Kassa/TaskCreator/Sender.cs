using System;
using RabbitMQ.Client;
using System.Text;
class Sender
{
    public static void Main(string[] args)
    {
        
            var factory = new ConnectionFactory() { HostName = "10.3.56.27", UserName = "manager", Password = "ehb" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            string xml = @"<?xml version='1.0'?>
                      <person id='1'>
                        <name>Alan</name>
                        <url>http://www.google.com</url>
                      </person>";
            return ((args.Length > 0)
                   ? string.Join(" ", args)
                   : xml);
        }
    }
