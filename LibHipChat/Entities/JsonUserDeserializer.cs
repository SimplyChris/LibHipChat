using System.Collections.Generic;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{ 
    public class JsonUserDeserializer <T> : IJsonDeserializer <IList<T>>
    {                
        public IList<T> Deserialize(string jsonString)
        {
            var jsonUserModel = (JsonConvert.DeserializeObject<JsonUsersModel>(jsonString));
            jsonUserModel.DeserializeModel();
            return (IList<T>) jsonUserModel.Model;
        }
    }
}
