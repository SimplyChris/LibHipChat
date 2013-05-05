using LibHipChat.XMPP.Interfaces;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Utility
{
    public static class MessageFactory
    {
        public static Message Create (agsXMPP.protocol.client.Message message)
        {
            var xmppMessage = new Message {ReplyEntity = new Entity()};

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
        
        private static string ExtractReplyTo (agsXMPP.protocol.client.Message message)
        {
            var messageParts = message.From.ToString().Split(new[] { '/' });
            return messageParts[0];          
        }

        private static string ExtractFromUser(agsXMPP.protocol.client.Message message)
        {
            var messageParts = message.From.ToString().Split(new[] { '/' });
            return messageParts[1];
        }
    }
}