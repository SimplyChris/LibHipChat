using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Interfaces
{
    public interface IXMPPMessageProcessor
    {
        void ProcessMessage(XMPPMessageContext context);
    }
}