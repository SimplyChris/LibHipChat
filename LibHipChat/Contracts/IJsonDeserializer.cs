using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Contracts
{
    public interface IJsonDeserializer
    {        
        IJsonModel Deserialize(string jsonString);
    }
}