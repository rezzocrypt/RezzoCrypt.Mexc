using RezzoCrypt.Mexc.Extensions;

namespace RezzoCrypt.Mexc.APIs
{
    public class MexcService
    {
        private readonly MexcConnection _connection;
        public MexcService(MexcConnection connection)
        {
            _connection = connection;
        }
        public string Ping() => _connection.GetUrlResult<string>("/api/v3/ping");

        public DateTime Time() => _connection.GetUrlResult<string>("/api/v3/time").DateFromTicks();
    }
}
