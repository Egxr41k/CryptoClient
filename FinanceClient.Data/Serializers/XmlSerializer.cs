namespace FinanceClient.Data.Serializers
{
    public class XmlSerializer<T> : ISerializer<T>
    {
        public async Task<T> DeserializeAsync(string path)
        {
            if (!File.Exists(path)) return default;

            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using var stream = new FileStream(path, FileMode.OpenOrCreate);

                var currencyModels = (T)serializer.Deserialize(stream);

                return currencyModels;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task SerializeAsync(T data, string path)
        {
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using var stream = new FileStream(path, FileMode.OpenOrCreate);
                serializer.Serialize(stream, data);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
