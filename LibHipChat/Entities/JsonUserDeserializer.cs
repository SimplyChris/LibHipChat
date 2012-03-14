using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{ 
    public class JsonUserDeserializer <T> : IJsonDeserializer <IList<T>>
    {                
        public IList<T> Deserialize(string jsonString)
        {
            var jsonUserModel = (JsonConvert.DeserializeObject<JsonUserModel>(jsonString));
            jsonUserModel.DeserializeList();
            return (IList<T>) jsonUserModel.Model;
        }
    }
}
