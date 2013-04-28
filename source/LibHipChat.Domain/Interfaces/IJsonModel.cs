using Newtonsoft.Json;

namespace LibHipChat.Domain.Interfaces
{
    public interface IJsonModel <T>
    {
        [JsonProperty]
        T Data { get; }

        void DeserializeModel ();
    }
}