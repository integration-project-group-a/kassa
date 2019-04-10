using System;
using RabbitMQ.Client;
using System.Text;
class Sender
{
    public static void Main(string[] args)
    {

        //var factory = new ConnectionFactory() { HostName = "10.3.56.27", UserName = "manager", Password = "ehb" };
        var factory = new ConnectionFactory() { HostName = "localhost" };
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
            string xml = @"<Visitor>
	                            <header>
		                            <MessageType>Visitor</MessageType>
		                            <description>Creation of a visitor</description>
		                            <sender>front-end</sender> <!-- kassa, crm, front-end -->
	                            </header>
	                            <datastructure>
		                            <UUID></UUID>
		                            <name>
			                            <firstname></firstname>
			                            <lastname></lastname>
		                            </name>
		                            <email></email>
		                            <timestamp></timestamp>
		                            <version></version>
		                            <isActive></isActive>
		                            <banned></banned>
		                            <!-- Not required fields -->
		                            <birthdate></birthdate>
		                            <btw-nummer></btw-nummer>
		                            <gsm-nummer></gsm-nummer>
		                            <GDPR></GDPR>
		                            <extraField></extraField>
	                            </datastructure>
                            </Visitor>";
            return ((args.Length > 0)
                   ? string.Join(" ", args)
                   : xml);
        }
    }
