using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Interfaces
{
    public interface IMessageProcessor
    {
        void ProcessMessage(MessageContext context);
    }
}