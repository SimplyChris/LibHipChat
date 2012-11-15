using LibHipChat.Services.Contracts;

namespace LibHipChat.WindowsService.Contracts
{
    public interface IRoomContext
    {
        IMessageProcessor MessageProcessor { get; set; }


    }
}