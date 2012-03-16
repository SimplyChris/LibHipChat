using System.Collections.Generic;
using LibHipChat.Entities;
using Newtonsoft.Json;

namespace LibHipChat.Contracts
{
    public interface IJsonModel <T>
    {
        [JsonProperty]
        T Data { get; }

        void DeserializeModel ();
    }
}