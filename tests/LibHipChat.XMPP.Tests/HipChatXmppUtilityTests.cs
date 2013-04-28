using LibHipChat.XMPP.Utility;
using NUnit.Framework;
using agsXMPP.protocol.client;

namespace LibHipChat.XMPP.Tests
{
    [TestFixture]
    public class HipChatXmppUtilityTests
    {
         
        [Test]
        public void should_parse_room_message_into_proper_type()
        {
            var message = new Message() {From = "18167_api_development@conf.hipchat.com/Dharma Soft", Body = "Message Body"};

            var xmppMessage = XmppMessageFactory.Create(message);
            Assert.That(xmppMessage.MessageType,Is.EqualTo(XmppMessageType.RoomMessage));
        }
        
        [Test]
        public void should_parse_direct_message_into_proper_type()
        {
            var message = new Message() {From = "18167_80295@chat.hipchat.com", Body = "Message Body"};
            var xmppMessage = XmppMessageFactory.Create(message);
            Assert.That(xmppMessage.MessageType, Is.EqualTo(XmppMessageType.DirectMessage));

        }
    }
}