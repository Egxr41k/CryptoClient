using System.Xml.Serialization;

namespace CryptoClient.Data.Services
{
    public class XmlService : IFetchService
    {
        private readonly HttpClient _httpClient;

        public XmlService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                var responseStream = await _httpClient.GetStreamAsync(url);

                var serializer = new XmlSerializer(typeof(T));
                T result;

                using (var streamReader = new StreamReader(responseStream))
                {
                    result = (T)serializer.Deserialize(streamReader);
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching data from {url}: {ex.Message}", ex);
            }
        }
    }
}
