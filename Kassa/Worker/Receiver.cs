using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using System.Xml;
using XmlrpcAPI.Models;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using Flurl.Http;
using System.Net.Http;
using System.Net.Http.Headers;

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
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(message);

                string MessageType = getMessageType(message).ToLower();
                switch (MessageType)
                {
                    case "visitor":
                        //https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object
                        XmlSerializer serializer = new XmlSerializer(typeof(XmlrpcAPI.Dto.Visitor));
                        TextReader reader = new StringReader(message);
                        XmlrpcAPI.Dto.Visitor visitor = (XmlrpcAPI.Dto.Visitor)serializer.Deserialize(reader);
                        /*
                        Console.WriteLine("\n\nMessageType: " + visitor.header.MessageType + "\nDescription: " + visitor.header.description + "\nSender: " + visitor.header.sender);
                        Console.WriteLine("UUID: " + visitor.datastructure.UUID + "\nFirst Name: " + visitor.datastructure.name.firstname + "\nLast Name: " + visitor.datastructure.name.lastname);
                        */
                        
                        string nameVisitor = visitor.datastructure.name.firstname + " " + visitor.datastructure.name.lastname;
                        string d = visitor.datastructure.dateOfBirth;
                        Console.WriteLine(visitor.datastructure.dateOfBirth.ToString());
                        

                        Customer valuesVisitor = new Customer(visitor.datastructure.UUID, 
                                                              nameVisitor, 
                                                              visitor.datastructure.email, 
                                                              Int32.Parse(visitor.datastructure.timestamp), 
                                                              Int32.Parse(visitor.datastructure.version), 
                                                              Convert.ToBoolean(visitor.datastructure.isActive), 
                                                              Convert.ToBoolean(visitor.datastructure.banned), 
                                                              DateTime.Parse(visitor.datastructure.dateOfBirth), 
                                                              visitor.datastructure.btwNumber, 
                                                              visitor.datastructure.gsmNumber);
                        
                        var responseString = await "https://localhost:44389/api/Customer"
                                    .WithHeader("Accept", "application/json")
                                    .PostJsonAsync(valuesVisitor)
                                    .ReceiveJson<object>();

                        Console.WriteLine(responseString);

                        break;

                    default:
                        Console.WriteLine("An error has occurred.");
                        break;
                }
                
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    public static string getMessageType(string message)
    {
        string MessageType = "";
        //https://stackoverflow.com/questions/10709821/find-text-in-string-with-c-sharp
        int Start, End;
        if (message.Contains("<MessageType>") && message.Contains("</MessageType>"))
        {
            Start = message.IndexOf("<MessageType>", 0) + "<MessageType>".Length;
            End = message.IndexOf("</MessageType>", Start);
            MessageType = message.Substring(Start, End - Start);
        }
        return MessageType;
    }
}