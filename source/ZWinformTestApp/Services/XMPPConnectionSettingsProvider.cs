using LibHipChat.XMPP;
using LibHipChat.XMPP.Interfaces;

namespace ZWinformTestApp.Services
{
    public class XMPPConnectionSettingsProvider : IConnectionSettingsProvider 
    {
        public HipChatXmppConnectionSettings GetConnectionSettings()
        {
            //TODO: Read from configuration
            return new HipChatXmppConnectionSettings()
                       {
                           UserName = "18167_192253",
                           Server = "chat.hipchat.com",
                           Password = "botuser123"
                       };
        }
    }
}