using System;
using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Proxy.Contracts
{
    //ToDO: Seperate / Rename Proxy.
    //Todo: Split out connection management from service calls

    public interface IHipChatProxy
    {        
        HipChatDeleteResponse DeleteUser(string userId);
        NewUser CreateUser(string email, string name, string title, string is_group_admin);
        User GetUser(int userId);
        HipChatStatus MessageRoom(string roomId, string from, string message);
        IList<User> GetUserList();
        IList<Room> GetRoomList();
        RoomDetail GetRoomInfo(string roomId);
        

        ErrorModel LastError { get; set; }
        Int32 ApiCallsRemaining { get; }
        int GetUserId(string email);
    }

}