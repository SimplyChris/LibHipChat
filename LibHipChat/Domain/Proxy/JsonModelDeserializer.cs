using LibHipChat.Domain.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Proxy
{
    public class JsonModelDeserializer <T>: IJsonDeserializer<T>
    {
        public T Deserialize(string jsonString)
        {
            var jsonModel = (JsonConvert.DeserializeObject<T>(jsonString));            
            return jsonModel;
        }
    }
}