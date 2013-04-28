using LibHipChat.Domain.Interfaces;
using Newtonsoft.Json;

namespace LibHipChat.Domain.Services
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