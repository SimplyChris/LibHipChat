using System.Collections.Generic;
using LibHipChat.Entities;
using Newtonsoft.Json;

namespace LibHipChat.Contracts
{
    public interface IJsonModel
    {
        [JsonProperty]
        IList<Dictionary<string, string>> Data { get; }

        void DeserializeList();
    }
}