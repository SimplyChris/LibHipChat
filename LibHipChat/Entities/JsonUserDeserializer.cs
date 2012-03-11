using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using LibHipChat.Contracts;
using Newtonsoft.Json;

namespace LibHipChat.Entities
{ 
    public class JsonUserDeserializer : IJsonDeserializer
    {                
        public IList<IHipChatModel> Deserialize(string jsonString)
        {
            var jsonUserModel = (JsonConvert.DeserializeObject<JsonUserModel>(jsonString));
            var users = jsonUserModel.DeserializeList();
            return users;
        }
    }
}
