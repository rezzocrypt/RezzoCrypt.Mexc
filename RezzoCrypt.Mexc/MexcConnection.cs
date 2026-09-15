using Flurl;
using Flurl.Http;
using RezzoCrypt.Mexc.APIs;
using RezzoCrypt.Mexc.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace RezzoCrypt.Mexc
{
    public class MexcConnection
    {
        internal string _baseUrl = "https://api.mexc.com";
        internal string _apiKey = "";
        internal string _apiSecret = "";

        public enum Method
        {
            Get,
            Post,
            Delete
        }

        #region Вспомогательные

        internal static string Sign(string source, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var hmacsha256 = new HMACSHA256(keyBytes);
            var sourceBytes = Encoding.UTF8.GetBytes(source);
            var hash = hmacsha256.ComputeHash(sourceBytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        internal async Task<T> GetUrlResultAsync<T>(string url, object? data = null, Method method = Method.Get, bool secure = false)
            where T : class
        {
            var currentRequest = _baseUrl
                .AppendPathSegment(url)
                .WithHeader("X-MEXC-APIKEY", _apiKey)
                .SetQueryParams(data);

            if (secure)
            {
                currentRequest.SetQueryParam("timestamp", DateTime.UtcNow.StringTicksFromDate());
                currentRequest.SetQueryParam("signature", Sign(currentRequest.Url.Query, _apiSecret));
            }

            try
            {
                var httpMethod = method switch
                {
                    Method.Post => HttpMethod.Post,
                    Method.Delete => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                var responseResult = await currentRequest.SendAsync(httpMethod);

                return typeof(T) == typeof(string)
                    ? (T)(object)await responseResult.GetStringAsync()
                    : await responseResult.GetJsonAsync<T>();
            }
            catch (FlurlHttpException fhttpex)
            {
                string? serverErrorMessage = null;

                if (fhttpex.Call.Response != null)
                {
                    try
                    {
                        serverErrorMessage = $"url: {url}, data: {currentRequest.Url.Query}, error: {await fhttpex.Call.Response.GetStringAsync()}";
                    }
                    catch
                    {
                        // Could not extract server side error, just continue with original exception.
                    }
                }

                if (!string.IsNullOrEmpty(serverErrorMessage))
                {
                    throw new Exception(serverErrorMessage, fhttpex);
                }

                throw;
            }
        }

        #endregion

        public MexcConnection(string apiKey = "", string apiSecret = "")
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }

        /// <summary>
        /// Account methods
        /// </summary>
        public MexcAccount Account => new(this);

        /// <summary>
        /// Exchange methods
        /// </summary>
        public MexcExchange Exchange => new(this);

        /// <summary>
        /// Data methods
        /// </summary>
        public MexcData Data => new(this);

        /// <summary>
        /// Service methods
        /// </summary>
        public MexcService Service => new(this);
    }
}