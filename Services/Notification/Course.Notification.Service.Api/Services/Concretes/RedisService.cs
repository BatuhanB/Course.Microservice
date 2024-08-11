using StackExchange.Redis;

namespace Course.Notification.Service.Api.Services.Concretes
{
    public class RedisService(string host, int port)
    {
        private readonly string _host = host;
        private readonly int _port = port;

        private ConnectionMultiplexer _ConnectionMultiplexer;

        public void Connect()
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                // Activate this part in development not in production
                EndPoints = { $"{_host}:{_port}" }
                //EndPoints = { $"{_host}" }
            };
            _ConnectionMultiplexer = ConnectionMultiplexer.Connect(options);
        }

        public IDatabase GetDb(int db = 2) => _ConnectionMultiplexer.GetDatabase(db);
    }
}
