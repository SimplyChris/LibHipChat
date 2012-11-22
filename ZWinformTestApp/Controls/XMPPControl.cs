using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibHipChat.XMPP;
using StructureMap;
using ZWinformTestApp.Services;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.iq.roster;
using Message = agsXMPP.protocol.client.Message;

namespace ZWinformTestApp.Controls
{     
    public partial class XMPPControl : UserControl
    {
        private HipChatXMPPConnection _xmppConnection;
        private HipChatXMPPRoomManager _xmppRoomManager;

        private delegate void SetTextCallback(string text, params object [] parameters);
        public XMPPControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _xmppConnection = new HipChatXMPPConnection(new XMPPConnectionSettingsProvider().GetConnectionSettings());
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
            _xmppConnection.OnRosterStart += delegate(object sender) { SetMessageText("Roster List Start"); };
            _xmppConnection.OnRosterItem += delegate(object sender, RosterItem item) { SetMessageText("Roster Item: {0}", item.Name); };
            _xmppConnection.OnRosterEnd += delegate(object sender) { SetMessageText("Roster List End"); };
            _xmppConnection.OnDirectMessageReceived += delegate(object sender, XmppMessage message) { SetMessageText("Direct Message - ReplyTo: [{0}] Message: [{1}]", message.ReplyEntity.ReplyTo, message.Body); };
            _xmppConnection.OnRoomMessageReceived += delegate(object sender, XmppMessage message){SetMessageText("Room Message - Reply To: [{0}] Display User: [{1}] Message: [{2}]", message.ReplyEntity.ReplyTo, message.ReplyEntity.FromUser ,message.Body);};
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
            if (this.tbOutput.InvokeRequired)
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
            _xmppRoomManager = new HipChatXMPPRoomManager(_xmppConnection);
            _xmppRoomManager.JoinRoom(new HipChatRoom() { Id = tbRoomToJoin.Text, NickName = "bot user" });            
        }

        private void bnLeaveRoom_Click(object sender, EventArgs e)
        {
            _xmppRoomManager = new HipChatXMPPRoomManager(_xmppConnection);
            _xmppRoomManager.LeaveRoom(new HipChatRoom() { Id = tbRoomToJoin.Text, NickName = "bot user" });            
        }
    }
}
