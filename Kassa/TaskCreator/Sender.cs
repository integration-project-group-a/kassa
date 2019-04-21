using System;
using RabbitMQ.Client;
using System.Text;
class Sender
{
    public static void Main(string[] args)
    {

        var factory = new ConnectionFactory() { HostName = "10.3.56.27", UserName = "manager", Password = "ehb" };
        //var factory = new ConnectionFactory() { HostName = "localhost" };
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
	    <header><!-- type of message -->
		<MessageType>Visitor</MessageType><!--What your Message does -->
		<description>Creation of a visitor</description><!--Who sent it--><!--(fronted, crm, facturatie, kassa, monitor, planning, uuid) -->
		<sender>front-end</sender><!-- kassa, crm, front-end -->
	</header>
	<datastructure><!-- required fields = UUID name + email & hashing. -->
		<UUID>200</UUID><!-- id of the user -->
		<name>
			<firstname>Anthe</firstname>
			<lastname>Boets</lastname>
		</name><!-- kassa , front-end -->
		<email>anthe.boets@student.ehb.be</email>
		<GDPR>true</GDPR>
		<timestamp>1999-04-30T00:00:00+0k0:00</timestamp>
		<version>s</version>
		<isActive>true</isActive>
		<banned>false</banned><!-- Not required fields -->
		<geboortedatum>1999-04-30T00:00:00+00:00</geboortedatum>
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
