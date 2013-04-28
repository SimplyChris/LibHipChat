using LibHipChat.XMPP.Interfaces;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP
{
    public class XMPPMessageContext
    {
        public Message Message { get; set; }
        public IXMPPChannelResponder ChannelResponder { get; set; }
    }
}