using System;

namespace LibHipChat.WindowsService.Configuration
{
    public class ProcessorConfiguration
    {
        public String ProcessorType { get; set; }
        public string RoomId { get; set; }
        public Int32 BufferTime { get; set; }
    }
}