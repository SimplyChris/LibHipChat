namespace LibHipChat.Domain.Contracts
{
    public interface IJsonDeserializer <T>
    {        
        T Deserialize(string jsonString);        
    }
}