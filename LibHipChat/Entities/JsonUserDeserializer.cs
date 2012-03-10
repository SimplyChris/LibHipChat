using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{
    [DataContract]
    public class JsonUserDeserializer : IJsonDeserializer
    {                
        public IJsonModel Deserialize(string jsonString)
        {
            var jsonUserModel = (JsonConvert.DeserializeObject<JsonUserModel>(jsonString));
            return jsonUserModel;
        }
    }
}