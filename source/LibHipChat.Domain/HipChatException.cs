using System;
using LibHipChat.Domain.Entities;

namespace LibHipChat.Domain
{
    public class HipChatException : Exception
    {
        public ErrorModel Error { get; set; }
        
        public HipChatException(ErrorModel errorModel, Exception innerException)
            : base(String.Format("HipChat Error -- Type:[{0}] Message:[{1}]", errorModel.ErrorType, errorModel.Message, innerException))
        {
            Error = errorModel;                    
        }
    }
}