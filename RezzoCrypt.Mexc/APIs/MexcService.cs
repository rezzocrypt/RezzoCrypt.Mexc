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

        public Task<string> PingAsync() => _connection.GetUrlResultAsync<string>("/api/v3/ping");

        public async Task<DateTime> TimeAsync()
        {
            var time = await _connection.GetUrlResultAsync<string>("/api/v3/time");
            return time.DateFromTicks();
        }
    }
}