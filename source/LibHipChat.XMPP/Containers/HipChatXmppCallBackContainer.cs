using agsXMPP;

namespace LibHipChat.XMPP.Containers
{
    public class HipChatXmppCallBackContainer
    {
        public MessageCB MessageCallBack { get; set; }
        public PresenceCB PresenceCallBack { get; set; }
    }
}