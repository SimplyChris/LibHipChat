using LibHipChat.XMPP.Interfaces;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Utility
{
    public static class MessageFactory
    {
        public static HipChatMessage Create (agsXMPP.protocol.client.Message message)
        {
            var xmppMessage = new HipChatMessage {ReplyEntity = new Entity()};

            xmppMessage.ReplyEntity.ReplyTo = ExtractReplyTo(message);

            if (message.From.ToString().Contains("conf.hipchat.com"))
            {
                xmppMessage.MessageType = MessageType.RoomMessage;
                xmppMessage.ReplyEntity.FromUser = ExtractFromUser(message);
            }
            
            else if (message.From.ToString().Contains("chat.hipchat.com"))
                xmppMessage.MessageType = MessageType.DirectMessage;
            else
                xmppMessage.MessageType = MessageType.UnKnown;

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