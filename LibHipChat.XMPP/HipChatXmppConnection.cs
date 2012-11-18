using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.Base;

namespace LibHipChat.XMPP
{
    public delegate void ConnectEventHandler(object sender, EventArgs e);

    public class HipChatXMPPConnection
    {
        private HipChatXmppConnectionSettings ConnectionSettings { get; set; }

        public XmppClientConnection ClientConnection { get; set; }

        public HipChatXMPPConnection (HipChatXmppConnectionSettings settings)
        {
            ConnectionSettings = settings;
            ClientConnection = new XmppClientConnection(settings.Server);
            ClientConnection.UseSSL = true;
            ClientConnection.UseStartTLS = true;
            ClientConnection.OnError += ReportError;
        }

        public void OpenConnection (string RoomId)
        {
            ClientConnection.Open(ConnectionSettings.UserName, ConnectionSettings.Password);            
            ClientConnection.OnLogin += AutoJoinRoom;

        }

        public void AutoJoinRoom(object sender)
        {
            
        }

        public void ReportError(object sender, Exception ex)
        {
            Console.WriteLine("XMPP Exception of type {0}. ", ex.GetType());
            Console.WriteLine(ex.Message);
        }

        public void CloseConnection ()
        {
            ClientConnection.Close();
        }
     
        private void AddRosterItem (object source, RosterItem rosterItem )
        {
            
        }
    }
}
