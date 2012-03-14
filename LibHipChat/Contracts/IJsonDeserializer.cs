using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Contracts
{
    public interface IJsonDeserializer <T>
    {        
        T Deserialize(string jsonString);        
    }
}