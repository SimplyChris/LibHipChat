using agsXMPP;

namespace LibHipChat.XMPP.Containers
{
    public class CallBackContainer
    {
        public MessageCB MessageCallBack { get; set; }
        public PresenceCB PresenceCallBack { get; set; }
    }
}