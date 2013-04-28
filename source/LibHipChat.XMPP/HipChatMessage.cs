namespace LibHipChat.XMPP
{
    public class HipChatMessage
    {
        public XmppMessageType MessageType { get; set; }
        public string Body { get; set; }
        public HipChatXmppEntity ReplyEntity { get; set; }
    }
}