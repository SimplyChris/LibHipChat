using System;
using LibHipChat.Domain.Entities;

namespace LibHipChat.Domain.Helpers
{
    public static class RoomMessageTypeIdentifier
    {
         public static RoomMessageType GetMessageType (RoomMessage message)
         {
             if (message.User.UserId == "api")
                 return RoomMessageType.ApiMessage;

             if (message.UploadInformation != null && ! String.IsNullOrWhiteSpace(message.UploadInformation.Name))
                 return RoomMessageType.FileUpload;

             return RoomMessageType.UserMessage;
         }
    }   
}