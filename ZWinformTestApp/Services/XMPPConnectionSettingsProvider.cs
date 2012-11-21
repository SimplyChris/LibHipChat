using LibHipChat.XMPP;
using LibHipChat.XMPP.Interfaces;

namespace ZWinformTestApp.Services
{
    public class XMPPConnectionSettingsProvider : IXMPPConnectionSettingsProvider 
    {
        public HipChatXmppConnectionSettings GetConnectionSettings()
        {
            return new HipChatXmppConnectionSettings()
                       {
                           UserName = "18167_192253",
                           Server = "chat.hipchat.com",
                           Password = "botuser123"
                       };
        }
    }
}