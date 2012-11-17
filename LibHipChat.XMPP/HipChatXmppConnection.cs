﻿using System;
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

        private XmppClientConnection _clientConnection;

        public HipChatXMPPConnection (HipChatXmppConnectionSettings settings)
        {
            ConnectionSettings = settings;
            _clientConnection = new XmppClientConnection(settings.Server);
        }

        public void OpenConnection ()
        {
            _clientConnection.Open(ConnectionSettings.UserName, ConnectionSettings.Password);                       
        }

        public void CloseConnection ()
        {
            _clientConnection.Close();
        }
     
        private void AddRosterItem (object source, RosterItem rosterItem )
        {
            
        }
    }
}
