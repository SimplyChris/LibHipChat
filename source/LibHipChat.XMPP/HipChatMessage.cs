namespace LibHipChat.XMPP
{
    public class HipChatMessage
    {
        public MessageType MessageType { get; set; }
        public string Body { get; set; }
        public Entity ReplyEntity { get; set; }
    }
}