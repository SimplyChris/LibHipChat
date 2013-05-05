using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibHipChat.XMPP;
using LibHipChat.XMPP.Containers;
using StructureMap;
using ZWinformTestApp.Services;
using agsXMPP;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.roster;
using Message = agsXMPP.protocol.client.Message;

namespace ZWinformTestApp.Controls
{     
    public partial class XMPPControl : UserControl
    {
        private Connection _xmppConnection;
        private RoomManager _xmppRoomManager;

        private delegate void SetTextCallback(string text, params object [] parameters);
        public XMPPControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _xmppConnection = new Connection(new XMPPConnectionSettingsProvider().GetConnectionSettings());
                _xmppRoomManager = new RoomManager(_xmppConnection, new CallBackContainer(){MessageCallBack = MessageCallBack, PresenceCallBack = PresenceCallBack});
                SetupEvents();                                                           
            } 
            
        }

        private void bnConnect_Click(object sender, EventArgs e)
        {
            if (!_xmppConnection.Connected)
                _xmppConnection.OpenConnection();            
        }

        public void SetupEvents()
        {            
            _xmppConnection.OnLogin += delegate { SetMessageText("On Login Called"); };
            _xmppConnection.OnError += delegate(object sender, Exception exception)
                                                            {
                                                                SetMessageText("On Error Called: {0}",                                                                                               exception.Message);
                                                            };
            _xmppConnection.OnAuthError += delegate(object sender, Element element)
                                                                {
                                                                    SetMessageText("On Auth Error Called: {0}",
                                                                                      element.ToString());
                                                                };
            _xmppConnection.OnRosterStart += delegate { SetMessageText("Roster List Start"); };            
            _xmppConnection.OnRosterItem += delegate(object sender, RosterItem item) { SetMessageText("Roster Item: {0}", item.Name); };
            _xmppConnection.OnRosterEnd += delegate { SetMessageText("Roster List End"); };
            _xmppConnection.OnDirectMessageReceived += delegate(object sender, LibHipChat.XMPP.HipChatMessage message) { SetMessageText("Direct Message - ReplyTo: [{0}] Message: [{1}]", message.ReplyEntity.ReplyTo, message.Body); };
            _xmppConnection.OnRoomMessageReceived += delegate(object sender, HipChatMessage message){SetMessageText("Room Message - Reply To: [{0}] Display User: [{1}] Message: [{2}]", message.ReplyEntity.ReplyTo, message.ReplyEntity.FromUser ,message.Body);};
            _xmppConnection.OnMessageReceived +=
                delegate(object sender, Message msg)
                    { SetMessageText("Message - From: {0} Body: {1}", msg.From, msg.Body); };                    
        }

        private void bnDisconnect_Click(object sender, EventArgs e)
        {
            _xmppConnection.CloseConnection();
        }

        private void SetMessageText(string format, params object[] parameters)
        {
            if (tbOutput.InvokeRequired)
            {
                SetTextCallback d = SetMessageText;
                Invoke(d, format, parameters);
            }
            else
            {
                var stringText = String.Format(format, parameters);
                tbOutput.Text += stringText + Environment.NewLine;
                tbOutput.SelectionStart = tbOutput.Text.Length;
                tbOutput.ScrollToCaret();
            }
        }

        private void bnJoinRoom_Click(object sender, EventArgs e)
        {            
            _xmppRoomManager.JoinRoom(new Room() { Id = tbRoomToJoin.Text, NickName = "bot user" });            
        }

        private void bnLeaveRoom_Click(object sender, EventArgs e)
        {
            _xmppRoomManager = new RoomManager(_xmppConnection);
            _xmppRoomManager.LeaveRoom(new Room() { Id = tbRoomToJoin.Text, NickName = "bot user" });            
        }

        private void MessageCallBack (object sender, Message message, object data)
        {
            SetMessageText("Message On Callback: {0} From: {1}", message.Body, message.From);
        }
        
        private void PresenceCallBack (object sender, Presence presence, object data )
        {
            SetMessageText("Presence On Callback: {0} From: {1}", presence.Nickname, presence.Status);
            
        }
    }
}
