namespace LibHipChat.XMPP
{
    public class XmppMessage
    {
        public XmppMessageType MessageType { get; set; }
        public string Body { get; set; }
        public HipChatXmppEntity ReplyEntity { get; set; }
    }
}