using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Contracts
{
    public interface IJsonDeserializer
    {        
        IList<IHipChatModel> Deserialize(string jsonString);

        
    }
}