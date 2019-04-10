using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using System.Xml;
using XmlrpcAPI.Models;
using Newtonsoft.Json.Linq;

class Receiver
{
    public static void Main()
    {
        //var factory = new ConnectionFactory() { HostName = "10.3.56.27", UserName = "manager", Password = "ehb" };
        var factory = new ConnectionFactory() { HostName = "localhost"};
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "logs", type: "fanout");

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "logs",
                              routingKey: "");

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(message);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                Console.WriteLine("Json:" + jsonText);
                JObject jo = JObject.Parse(jsonText);
                

                /*
                 Dto.ShowCustomer tempCustomer = new Dto.ShowCustomer(jo["name"].ToString(), jo["x_UUID"].ToString(), Int32.Parse(jo["x_timestamp"].ToString()),
                    Int32.Parse(jo["x_version"].ToString()), bool.Parse(jo["x_banned"].ToString()), bool.Parse(jo["active"].ToString()), 
                    jo["email"].ToString(), jo["mobile"].ToString(), jo["x_dateofbirth"].ToString(), jo["vat"].ToString());
                 */
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}