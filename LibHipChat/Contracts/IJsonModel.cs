using Newtonsoft.Json;

namespace LibHipChat.Domain.Contracts
{
    public interface IJsonModel <T>
    {
        [JsonProperty]
        T Data { get; }

        void DeserializeModel ();
    }
}