using LibHipChat.XMPP.Interfaces;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Utility
{
    public static class XmppMessageFactory
    {
        public static HipChatMessage Create (Message message)
        {
            var xmppMessage = new HipChatMessage {ReplyEntity = new HipChatXmppEntity()};

            xmppMessage.ReplyEntity.ReplyTo = ExtractReplyTo(message);

            if (message.From.ToString().Contains("conf.hipchat.com"))
            {
                xmppMessage.MessageType = XmppMessageType.RoomMessage;
                xmppMessage.ReplyEntity.FromUser = ExtractFromUser(message);
            }
            
            else if (message.From.ToString().Contains("chat.hipchat.com"))
                xmppMessage.MessageType = XmppMessageType.DirectMessage;
            else
                xmppMessage.MessageType = XmppMessageType.UnKnown;

            xmppMessage.Body = message.Body;            
            return xmppMessage;
        }
        
        private static string ExtractReplyTo (Message message)
        {
            var messageParts = message.From.ToString().Split(new[] { '/' });
            return messageParts[0];          
        }

        private static string ExtractFromUser(Message message)
        {
            var messageParts = message.From.ToString().Split(new[] { '/' });
            return messageParts[1];
        }
    }
}