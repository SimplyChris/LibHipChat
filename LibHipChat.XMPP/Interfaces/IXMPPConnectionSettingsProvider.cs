namespace LibHipChat.XMPP.Interfaces
{
    public interface IXMPPConnectionSettingsProvider
    {
        HipChatXmppConnectionSettings GetConnectionSettings();
    }
}