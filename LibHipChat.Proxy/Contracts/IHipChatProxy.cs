﻿using System.Collections.Generic;
using LibHipChat.Entities;

namespace LibHipChat.Proxy.Contracts
{
    //ToDO: Seperate / Rename Proxy.
    //Todo: Split out connection management from service calls

    public interface IHipChatProxy
    {        
        HipChatDeleteResponse DeleteUser(string userId);
        NewUser CreateUser(string email, string name, string title, string is_group_admin);
        HipChatStatus MessageRoom(string roomId, string from, string message);
        IList<User> GetUserList();
        IList<Room> GetRoomList();
        RoomDetail GetRoomInfo(string roomId);        
    }

}