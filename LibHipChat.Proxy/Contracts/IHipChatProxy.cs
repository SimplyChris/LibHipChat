using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Proxy.Contracts
{

    public interface IHipChatProxy
    {        
        HipChatResponse DeleteUser(string userId);
        NewUser AddUser(string email, string name, string title, string is_group_admin);
        HipChatStatus MessageRoom(string roomId, string from, string message);
        IList<User> GetUsers();
        IList<Room> GetRooms();
        RoomDetail GetRoomInfo(string roomId);
    }

}