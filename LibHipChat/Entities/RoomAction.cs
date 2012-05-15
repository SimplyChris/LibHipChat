﻿using System;

namespace LibHipChat.Entities
{
    public class RoomAction
    {
        public DateTime Date { get; set; }
        public User From { get; set; }
        public ActionType Type { get; set; }
    }

    public enum ActionType 
    {
        Message,
        FileUpload
    }
}