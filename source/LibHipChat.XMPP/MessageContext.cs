using LibHipChat.XMPP.Interfaces;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP
{
    public class MessageContext
    {
        public agsXMPP.protocol.client.Message Message { get; set; }
        public IChannelResponder ChannelResponder { get; set; }
    }
}