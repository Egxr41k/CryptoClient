using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Serializers
{
    public class XmlSerializer : ISerializer
    {
        public async Task<CurrencyModel[]> DeserializeAsync(string path)
        {
            if (!File.Exists(path)) return Array.Empty<CurrencyModel>();
            using var stream = File.OpenRead(path);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CurrencyModel[]));
            return (CurrencyModel[])serializer.Deserialize(stream) ?? Array.Empty<CurrencyModel>();
        }

        public async Task SerializeAsync(CurrencyModel[] data, string path)
        {
            using var stream = File.Create(path);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CurrencyModel[]));
            serializer.Serialize(stream, data);
        }
    }
}
