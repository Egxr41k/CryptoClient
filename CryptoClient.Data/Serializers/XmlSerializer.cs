using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CryptoClient.Data.Serializers
{
    public class XmlSerializer : ISerializer
    {
        public async Task<CurrencyModel[]> DeserializeAsync(string path)
        {
            if (!File.Exists(path)) return Array.Empty<CurrencyModel>();

            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CurrencyModel[]));

                using var stream = new FileStream(path, FileMode.OpenOrCreate);

                var currencyModels = serializer.Deserialize(stream) as CurrencyModel[];

                return currencyModels;
            }
            catch (Exception ex)
            {
                return Array.Empty<CurrencyModel>();
            }
        }

        public async Task SerializeAsync(CurrencyModel[] data, string path)
        {
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CurrencyModel[]));

                using var stream = new FileStream(path, FileMode.OpenOrCreate);
                serializer.Serialize(stream, data);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
