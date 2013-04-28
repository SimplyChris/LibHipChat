namespace LibHipChat.Domain.Interfaces
{
    public interface IJsonDeserializer <T>
    {        
        T Deserialize(string jsonString);        
    }
}