using LibHipChat.XMPP.Interfaces;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Utility
{
    public class XmppMessageFactory
    {
        public static XmppMessage Create (Message message)
        {
            var xmppMessage = new XmppMessage();

            xmppMessage.ReplyEntity = new HipChatXmppEntity();
            var messageParts = message.From.ToString().Split(new char[] { '/' });
            xmppMessage.ReplyEntity.ReplyTo = messageParts[0];
            if (message.From.ToString().Contains("conf.hipchat.com"))
            {
                xmppMessage.MessageType = XmppMessageType.RoomMessage;                
                xmppMessage.ReplyEntity.FromUser = messageParts[1];
            }
            else if (message.From.ToString().Contains("chat.hipchat.com"))
                xmppMessage.MessageType = XmppMessageType.DirectMessage;
            else
                xmppMessage.MessageType = XmppMessageType.UnKnown;

            xmppMessage.Body = message.Body;            
            return xmppMessage;
        }
    }
}