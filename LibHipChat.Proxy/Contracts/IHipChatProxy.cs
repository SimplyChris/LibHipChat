namespace LibHipChat.Proxy.Contracts
{

    public interface IHipChatProxy
    {        
        HipChatResponse DeleteUser(string userId);
        HipChatResponse AddUser(string userId);
        HipChatResponse MessageRoom(string roomId, string from, string message);
        HipChatResponse GetUsers();
        HipChatResponse GetRooms();
    }

}