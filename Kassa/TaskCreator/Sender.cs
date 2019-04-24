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
            string xml = @"<Message>
	                        <header>
		                    <MessageType>Visitor</MessageType>
		                    <description>Creation of a visitor</description>
		                    <sender>front-end</sender>
	                        </header>
	                        <datastructure>
		                    <UUID>200</UUID>
		                    <name>
			                    <firstname>Anthe</firstname>
			                    <lastname>Boets</lastname>
		                    </name>
		                    <email>anthe.boets@student.ehb.be</email>
		                    <GDPR>true</GDPR>
		                    <timestamp>1553001960</timestamp>
		                    <version>1</version>
		                    <isActive>true</isActive>
		                    <banned>false</banned><!-- Not required fields -->
		                    <geboortedatum>30/04/1999</geboortedatum>
		                    <btw-nummer>BE15656464654</btw-nummer>
		                    <gsm-nummer>015313164165468</gsm-nummer>
		                    <extraField></extraField>
	                        </datastructure>
                           </Message>";
            return ((args.Length > 0)
                   ? string.Join(" ", args)
                   : xml);
        }
    }
