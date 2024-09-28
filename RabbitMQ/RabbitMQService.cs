using RabbitMQ.Client;
using System.Text;

public class RabbitMQService : IRabbitMQService
{
	private readonly string _queueName = "user-queue";
	private readonly IConnection _connection;
	private readonly IModel _channel;

    public RabbitMQService()
	{
		var factory = new ConnectionFactory() { HostName = "192.168.32.133" }; // Сервис RabbitMQ
		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(
			queue: _queueName,
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null
		);
	}

    public void SendMessage(string message)
	{
		var body = Encoding.UTF8.GetBytes(message);
		_channel.BasicPublish(
			exchange: "",
			routingKey: _queueName,
			basicProperties: null,
			body: body
		);
	}
}