using System;
using System.Text;
using RabbitMQ.Client;

namespace QtES.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = string.Empty;
            System.Console.WriteLine("Opprett nye Rabbit-meldinger ved å skrive noe og trykk <enter>. CTRL+C for å avbryte");
            while (!string.IsNullOrEmpty(message = System.Console.ReadLine()))
            {
                try
                {
                    var factory = new ConnectionFactory() {HostName = "localhost"};
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare("logs", "fanout");
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish("logs", "", null, body);
                        System.Console.WriteLine("OK. Klar for ny melding");
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Feil. " + e.Message);
                }
            }
        }
    }
}
