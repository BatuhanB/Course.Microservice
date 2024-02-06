using StackExchange.Redis;

namespace Course.Order.Service.Api.Services.Concretes
{
    public class RedisService(string host, int port)
    {
        private readonly string _host = host;
        private readonly int _port = port;

        private ConnectionMultiplexer _ConnectionMultiplexer;

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);
    }
}
