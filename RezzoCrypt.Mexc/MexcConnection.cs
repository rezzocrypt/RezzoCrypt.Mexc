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

        internal T GetUrlResult<T>(string url, object? data = null, Method method = Method.Get, bool secure = false)
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
                var responseResult = method switch
                {
                    Method.Post => data != null
                        ? currentRequest.PostUrlEncodedAsync(data).Result
                        : currentRequest.PostAsync().Result,
                    _ => currentRequest.SetQueryParams(data).GetAsync().Result,
                };

                return typeof(T) == typeof(string)
                    ? (T)(responseResult.GetStringAsync().Result as object)
                    : responseResult.GetJsonAsync<T>().Result;
            }
            catch (Exception ex)
            {
                if (ex is FlurlHttpException fhttpex)
                {
                    string serverErrorMessage = string.Empty;
                    try
                    {
                        serverErrorMessage = $"url: {url}, data: {currentRequest.Url.Query}, error: {fhttpex.Call.Response.GetStringAsync().Result}";
                    }
                    catch
                    {
                        // Could not extract server side error , just continue with original exception.
                    }

                    if (serverErrorMessage != null)
                    {
                        throw new Exception(serverErrorMessage);
                    }
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