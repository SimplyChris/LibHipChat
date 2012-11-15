﻿using System;
using System.Collections.Generic;
using LibHipChat.Domain;
using LibHipChat.Domain.Entities;
using LibHipChat.Domain.Proxy;

namespace LibHipChat.Proxy.Contracts
{
    public interface IHipChatProxy
    {
        ErrorModel LastError { get; set; }
        Int32 ApiCallsRemaining { get; }

        HipChatDeleteResponse DeleteUser(string userId);
        NewUser CreateUser(string email, string name, string title, string is_group_admin);
        User GetUser (string userId);
        HipChatStatus MessageRoom(string roomId, string from, string message, MessageFormat format = MessageFormat.Html);
        IList<User> GetUserList();  
        IList<Room> GetRoomList();
        RoomDetail GetRoomInfo(string roomId);
        IList<RoomMessage> GetRecentRoomHistory(string roomid);
        IList<RoomMessage> GetRoomHistory(string roomid, DateTime date);
        String GetUserId(string email);
        void SetRoomTopic(string roomid, string newtopic);
    }

}