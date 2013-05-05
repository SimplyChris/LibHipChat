namespace LibHipChat.XMPP.Interfaces
{
    public interface IConnectionSettingsProvider
    {
        HipChatXmppConnectionSettings GetConnectionSettings();
    }
}